using Dapper.Base;
using Dapper.Base.Models;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ModelConvertToDto.Helpers
{
    public class CreateClassHelper
    {
        public static ITableDapperHelper dapperHelper = new DapperOracleHelper();
        //public static ITableDapperHelper dapperHelper = new DapperKingBaseHelper();

        private static List<string> excludes = new List<string>() { "ID", "ZUHUID", "ZUHUMC", "CHUANGJIANREN", "CHUANGJIANRXM", "CHUANGJIANSJ", "CHUANGJIANRID", "CHUANGJIANXTID", "XIUGAIREN", "XIUGAIRXM", "XIUGAISJ", "XIUGAIRID", "XIUGAIXTID", "ZUOFEIBZ" };

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="tableDapperHelper"></param>
        public static void Init(ITableDapperHelper tableDapperHelper)
        {
            dapperHelper = tableDapperHelper;
        }

        /// <summary>
        /// 判断数据库是否连接成功
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static List<TableDto> TestLianJie(string nameSpace)
        {
            // 获取全部表
            var tableList = dapperHelper.GetAllTableList();
            // 获取全部视图
            var viewList = dapperHelper.GetAllViewList();
            // 将表加载到列表中
            if (tableList != null && tableList.Count > 0)
            {
                tableList.ForEach(item =>
                {
                    SetTableName(item);
                });
            }
            // 将视图加载到列表中
            if (viewList != null && viewList.Count > 0)
            {
                viewList.ForEach(item =>
                {
                    SetTableName(item);
                });
            }
            return tableList;
        }

        #region 处理dto
        /// <summary>
        /// 处理单个Dto
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="tableName"></param>
        /// <param name="path"></param>
        public static void SaveTableDto(string nameSpace, string tableName, string path = "")
        {
            var table = dapperHelper.GetTable(tableName);
            SaveDtoFile(nameSpace, table, path);
        }

        /// <summary>
        /// 处理全部dto
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="path"></param>
        public static void SaveAllTableDto(string nameSpace, string path = "")
        {
            var tableList = dapperHelper.GetAllTableList();
            tableList.ForEach(x =>
            {
                SaveDtoFile(nameSpace, x, path, "Dto");
                SaveDtoFile(nameSpace, x, path, "UpdateDto");
                SaveDtoFile(nameSpace, x, path, "CreateDto");
            });
        }

        /// <summary>
        /// 保存Dto
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="lastName"></param>
        private static void SaveDtoFile(string nameSpace, TableDto table, string path = "", string lastName = "Dto")
        {
            SetTableName(table);
            var colList = dapperHelper.GetColList(table.TableName).Where(x => !excludes.Contains(x.ColumnName.ToUpper())).ToList();
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Directory.GetCurrentDirectory() + "\\DownFile\\";
            }
            path += $"\\Dto\\{table.FileName}s\\";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileContent = GetDtoFileContent(nameSpace, table, colList, path, lastName);

            //生成txt文件，将json字符串数据保存到txt文件
            string postPath = $"{path}{table.ModelName}{lastName}.cs";//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(fileContent);//Obj为json数据
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Console.WriteLine($"保存文件成功{path}：{table.ModelName}");
        }

        /// <summary>
        /// 生成dto内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="path"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private static string GetDtoFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", string lastName = "Dto")
        {
            var dtoStr = "";
            dtoStr += $@"using Mediinfo.Business.SaaS.API.Dto;
using System;";

            dtoStr += $@"
namespace {nameSpace}.API.Dtos
{{
    /// <summary>
    /// {table.Comments}
    /// </summary>
    public class {table.ModelName}{lastName}:BaseDto
    {{";
            foreach (var temp in colList)
            {
                dtoStr += $@"
        /// <summary>
        /// {temp.Comments}
        /// </summary>
        {GetCol(temp, true)}";
            }
            dtoStr += $@"
    }}";
            dtoStr += $@"
}}";
            return dtoStr;
        }
        #endregion

        #region 处理Model
        /// <summary>
        /// 处理单个Model
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="tableName"></param>
        /// <param name="path"></param>
        public static void SaveTableModel(string nameSpace, string tableName, string path = "")
        {
            var table = dapperHelper.GetTable(tableName);
            SaveModelFile(nameSpace, table, path);
        }

        /// <summary>
        /// 处理多个Model
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="path"></param>
        public static void SaveAllTableModel(string nameSpace, string path = "")
        {
            var tableList = dapperHelper.GetAllTableList();
            tableList.ForEach(x =>
            {
                SaveModelFile(nameSpace, x, path);
            });
        }

        /// <summary>
        /// 保存Model
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="path"></param>
        private static void SaveModelFile(string nameSpace, TableDto table, string path = "")
        {
            SetTableName(table);
            var colList = dapperHelper.GetColList(table.TableName).Where(x => !excludes.Contains(x.ColumnName.ToUpper())).ToList();
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Directory.GetCurrentDirectory() + "\\DownFile\\";
            }
            path += "\\Model\\";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileContent = GetModelFileContent(nameSpace, table, colList, path);

            //生成txt文件，将json字符串数据保存到txt文件
            string postPath = $"{path}{table.ModelName}Model.cs";//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(fileContent);//Obj为json数据
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Console.WriteLine($"保存文件成功{path}：{table.ModelName}");
        }

        /// <summary>
        /// 生成model内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="isDto">是否是Dto实体</param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        private static string GetModelFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", bool isDto = true)
        {
            var dtoStr = "";
            dtoStr += $@"using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
";
            dtoStr += $@"
namespace {nameSpace}.ORM.Models
{{
    /// <summary>
    /// {table.Comments}
    /// </summary>
    [Description(""{table.Comments}"")]
    [Table(""{table.TableName}"")]
    public class {table.ModelName}Model: MTEntity<string>
    {{";
            foreach (var temp in colList)
            {
                dtoStr += $@"
        /// <summary>
        /// {temp.Comments}
        /// </summary>
        [Description(""{temp.Comments}"")]
        {GetCol(temp, true)}";
            }
            dtoStr += $@"
    }}";
            dtoStr += $@"
}}";
            return dtoStr;
        }

        #endregion

        #region 生成Repositories
        /// <summary>
        /// 处理多个Repositories
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="like"></param>
        /// <param name="path"></param>
        public static void SaveTableListRepositories(string nameSpace, string like, string path = "")
        {
            var tableList = dapperHelper.GetQueryTableName(like);
            //path = Directory.GetCurrentDirectory() + path;
            tableList.ForEach(x =>
            {
                SaveRepositoriesFile(nameSpace, x, path);
            });
        }

        /// <summary>
        /// 保存Repositories
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="path"></param>
        private static void SaveRepositoriesFile(string nameSpace, TableDto table, string path = "")
        {
            SetTableName(table);
            var colList = dapperHelper.GetColList(table.TableName).Where(x => !excludes.Contains(x.ColumnName.ToUpper())).ToList();
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Directory.GetCurrentDirectory() + "\\DownFile\\";
            }
            path += "\\Repositories\\";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileContent = GetRepositoriesFileContent(nameSpace, table, colList, path);

            //生成txt文件，将json字符串数据保存到txt文件
            string postPath = $"{path}{table.ModelName}Repositories.cs";//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(fileContent);//Obj为json数据
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Console.WriteLine($"保存文件成功{path}：{table.ModelName}");
        }

        /// <summary>
        /// 生成Repositories内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="isDto">是否是Dto实体</param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static string GetRepositoriesFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", bool isDto = true)
        {
            var item = table.FileName.Substring(0, 1).ToLower() + table.FileName.Substring(1);

            var dtoStr = "";
            dtoStr += $@"using Mediinfo.Business.SaaS.ORM.Repositories;
using {nameSpace}.ORM.IRepositories;
using {nameSpace}.ORM.Models;
using Mediinfo.Starter.AutoDI.Attributes;
using System;
"; dtoStr += $@"
namespace {nameSpace}.ORM.Repositories
{{
    /// <summary>
    /// {table.Comments}
    /// </summary>
    [Repository]
    public class {table.ModelName}Repository : SaaSRepository<{nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext, {table.ModelName}Model, string>, I{table.ModelName}Repository
    {{"; dtoStr += $@"   
        public {table.ModelName}Repository(IServiceProvider serviceProvider) : base(serviceProvider)
        {{
        }}
    }}"; dtoStr += $@"
}}";
            return dtoStr;
        }

        #endregion

        #region 生成IRepositories
        /// <summary>
        /// 处理多个IRepositories
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="like"></param>
        /// <param name="path"></param>
        public static void SaveTableListIRepositories(string nameSpace, string like, string path = "")
        {
            var tableList = dapperHelper.GetQueryTableName(like);
            //path = Directory.GetCurrentDirectory() + path;
            tableList.ForEach(x =>
            {
                SaveIRepositoriesFile(nameSpace, x, path);
            });
        }

        /// <summary>
        /// 保存IRepositories
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="path"></param>
        private static void SaveIRepositoriesFile(string nameSpace, TableDto table, string path = "")
        {
            SetTableName(table);
            var colList = dapperHelper.GetColList(table.TableName).Where(x => !excludes.Contains(x.ColumnName.ToUpper())).ToList();
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Directory.GetCurrentDirectory() + "\\DownFile\\";
            }
            path += "\\IRepositories\\";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileContent = GetIRepositoriesFileContent(nameSpace, table, colList, path);

            //生成txt文件，将json字符串数据保存到txt文件
            string postPath = $"{path}I{table.ModelName}Repository.cs";//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(fileContent);//Obj为json数据
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Console.WriteLine($"保存文件成功{path}：{table.ModelName}");
        }

        /// <summary>
        /// 生成IRepositories内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="isDto">是否是Dto实体</param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static string GetIRepositoriesFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", bool isDto = true)
        {
            var item = table.FileName.Substring(0, 1).ToLower() + table.FileName.Substring(1);

            var dtoStr = "";
            dtoStr += $@"using Mediinfo.Business.SaaS.ORM.IRepositories;
using {nameSpace}.ORM.Models;
"; dtoStr += $@"
namespace {nameSpace}.ORM.IRepositories
{{
    /// <summary>
    /// {table.Comments}
    /// </summary>
    public interface I{table.ModelName}Repository : ISaaSRepository<{table.ModelName}Model, string>
    {{"; dtoStr += $@"   
    }}"; dtoStr += $@"
}}";
            return dtoStr;
        }

        #endregion

        #region 生成DbContext
        /// <summary>
        /// 处理DbContext
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="like"></param>
        /// <param name="path"></param>
        public static void SaveTableListDbContext(string nameSpace, string like, string path = "")
        {
            var tableList = dapperHelper.GetQueryTableName(like);
            //path = Directory.GetCurrentDirectory() + path;
            tableList.ForEach(x =>
            {
                SaveDbContextFile(nameSpace, x, path);
            });
        }

        /// <summary>
        /// 保存DbContext
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="path"></param>
        private static void SaveDbContextFile(string nameSpace, TableDto table, string path = "")
        {
            SetTableName(table);
            var colList = dapperHelper.GetColList(table.TableName).Where(x => !excludes.Contains(x.ColumnName.ToUpper())).ToList();
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Directory.GetCurrentDirectory() + "\\DownFile\\";
            }
            path += "\\DbContext\\";
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileContent = GetDbContextFileContent(nameSpace, table, colList, path);

            //生成txt文件，将json字符串数据保存到txt文件
            string postPath = $"{path}{table.ModelName}DbContext.cs";//路径+文件名
            byte[] bytes = null;
            bytes = Encoding.UTF8.GetBytes(fileContent);//Obj为json数据
            FileStream fs = new FileStream(postPath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            Console.WriteLine($"保存文件成功{path}：{table.ModelName}");
        }

        /// <summary>
        /// 生成DbContext内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="isDto">是否是Dto实体</param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static string GetDbContextFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", bool isDto = true)
        {
            var item = table.FileName.Substring(0, 1).ToLower() + table.FileName.Substring(1);

            var dtoStr = "";
            dtoStr += $@"using JetBrains.Annotations;
using MCRP.MSF.Core.UnitOfWork.Abstraction;
using {nameSpace}.ORM.Models;
using Mediinfo.SPH.Core.EntityFramework.Dynamic.Extensions;
using Mediinfo.SPH.FormLog.ORM;
using Microsoft.EntityFrameworkCore;
using System;
"; dtoStr += $@"
namespace {nameSpace}.ORM.ORM
{{
    public class {nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext : FormLogDbContext<{nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext>, IUnitOfWork<{nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext>, IUnitOfWork
    {{"; dtoStr += $@"  
        /// <summary>
        /// {table.Comments}
        /// </summary>
        public DbSet<{table.ModelName}Model> {table.ModelName}Models {{get; set; }}

        public {nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext([NotNull] DbContextOptions<{nameSpace.Split('.')[nameSpace.Split('.').Length - 1]}DbContext> options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {{
        }}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {{
            modelBuilder.InitDynamicEntity(this);
            base.OnModelCreating(modelBuilder);
        }}
    }}"; dtoStr += $@"
}}";
            return dtoStr;
        }
        #endregion

        public static List<string> GetTableByCol(string colName)
        {
            var table = dapperHelper.GetTableByCol(colName);
            return table.ToList();
        }









       

        /// <summary>
        /// 获取生成后的内容
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="table"></param>
        /// <param name="colList"></param>
        /// <param name="isDto">是否是Dto实体</param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static string GetServiceFileContent(string nameSpace, TableDto table, List<ColDto> colList, string path = "", bool isDto = true)
         {
            var item = table.FileName.Substring(0, 1).ToLower() + table.FileName.Substring(1);

            var dtoStr = "";
            dtoStr += $@"using Mediinfo.Business.SaaS.ORM.Repositories;;
using {nameSpace}.ORM.IRepositories;
using {nameSpace}.ORM.Models;
using Mediinfo.Starter.AutoDI.Attributes;
using System;
"; dtoStr += $@"

namespace {nameSpace}.ORM.Repositories
{{
    /// <summary>
    /// {table.Comments}
    /// </summary>
    [Table(""{table.TableName}"")]
    public class {table.ModelName}Service
    {{"; dtoStr += $@"
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaaSIdentityService _bmisIdentityService;
        private readonly ILA_RX_BingShiQKXXRepository _bingShiQKXXRepository;//病史
        private readonly ILA_RX_CaiChaoJCXXRepository _caiChaoJCXXRepository;//彩超
        private readonly ILA_RX_LinChuangJCXXRepository _linChuangJCXXRepository;//临床
        private readonly ILA_RX_SuiFangZLXXRepository _suiFangZLXXRepository;//随访
        private readonly ILA_RX_XXianJCXXRepository _xXianJCXXRepository;//X线

        /// <summary>
        /// 构造函数
        /// </summary>
        public RuXianAiService(
            IUnitOfWork unitOfWork,
            ISaaSIdentityService bmisIdentityService,
            ILA_RX_CaiChaoJCXXRepository caiChaoJCXXRepository,
            ILA_RX_LinChuangJCXXRepository linChuangJCXXRepository,
            ILA_RX_SuiFangZLXXRepository suiFangZLXXRepository,
            ILA_RX_XXianJCXXRepository xXianJCXXRepository,
            ILA_RX_BingShiQKXXRepository bingShiQKXXRepository
            )
        {{
            _unitOfWork = unitOfWork;
            _bmisIdentityService = bmisIdentityService;
            _caiChaoJCXXRepository = caiChaoJCXXRepository;
            _linChuangJCXXRepository = linChuangJCXXRepository;
            _suiFangZLXXRepository = suiFangZLXXRepository;
            _xXianJCXXRepository = xXianJCXXRepository;
            _bingShiQKXXRepository = bingShiQKXXRepository;
        }}
        
        #region {table.Comments}
            
        /// <summary>
        /// 新增{table.Comments}
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        public async Task<string> Add{table.FileName}({table.ModelName}CreateDto dto)
        {{
            var {item} = dto.MapTo<{table.ModelName}CreateDto, {table.ModelName}Model>();
            {item}.ZuZhiJGID = _bmisIdentityService.GetJiGouID();
            {item}.ZuZhiJGMC = _bmisIdentityService.GetJiGouMC();
            {item}.ShuJuLY = 0;
            var result = await _{item}Repository.InsertAsync({item});
            await _unitOfWork.SaveChangesWithLogAsync(result.Id);
            return result.Id;
        }}

        /// <summary>
        /// 编辑{table.Comments}
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        public async Task<int> Update{table.FileName}({table.ModelName}UpdateDto dto)
        {{
            var {item} = await _{item}Repository.GetAsync(dto.Id);
            if ({item} == null)
            {{
                throw ExceptionHelper.ThrowWeiZhaoDYC(""该记录不存在"");
            }}
            {item}.Merge(dto);
            var result = await _{item}Repository.UpdateAsync({item});
            return await _unitOfWork.SaveChangesWithLogAsync(result.Id);
        }}

        /// <summary>
        /// 获取{table.Comments}
        /// </summary>
        /// <param name=""dengJiID""></param>
        /// <returns></returns>
        public async Task<{table.ModelName}Dto> Get{table.FileName}(string dengJiID)
        {{
            var {item} = await _{item}Repository.GetAsNoTrackingAsync<{table.ModelName}Dto>(s => s.DengJiID == dengJiID);
            if ({item} == null)
            {{
                throw ExceptionHelper.ThrowWeiZhaoDYC(""该记录不存在"");
            }}
            return {item};
        }}

        /// <summary>
        /// 作废{table.Comments}
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        public async Task<int> ZuoFei{table.FileName}(string id)
        {{
            var {item}ID = await _{item}Repository.Where(s => s.Id == id).Select(s => s.Id).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty({item}ID))
            {{
                throw ExceptionHelper.ThrowWeiZhaoDYC(""该记录不存在"");
            }}
            await _{item}Repository.DeleteAsync({item}ID);
            return await _unitOfWork.SaveChangesWithLogAsync(id);
        }}

        #endregion";dtoStr += $@"
    }}";dtoStr += $@"
}}";
            return dtoStr;
        }

        public static void SaveTableListModel(string nameSpace, string like, string path = "")
        {
            var tableList = dapperHelper.GetQueryTableName(like);
            path = Directory.GetCurrentDirectory() + path;
            tableList.ForEach(x =>
            {
                SaveModelFile(nameSpace, x, path);
            });
        }

        public static void SaveTableListDto(string nameSpace, string like, string path = "")
        {
            var tableList = dapperHelper.GetQueryTableName(like);
            tableList.ForEach(x =>
            {
                SaveDtoFile(nameSpace, x, path, "Dto");
                SaveDtoFile(nameSpace, x, path, "UpdateDto");
                SaveDtoFile(nameSpace, x, path, "CreateDto");
            });
        }

        public static void SaveAllViewModel(string nameSpace, string path = "")
        {
            var tableList = dapperHelper.GetAllViewList();
            tableList.ForEach(x =>
            {
                SaveModelFile(nameSpace, x, path);
            });
        }







        #region 公共方法
        /// <summary>
        /// 获取字段相关信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static string GetCol(ColDto dto, bool isDto = true)
        {
            var columnName = dto.ColumnName;
            //有注释并且需要转换dto字段
            if (isDto)
            {
                columnName = GetColumnName(dto.Comments, dto.ColumnName);
            }
            var type = dapperHelper.GetColType(dto);
            return $"public {type} {columnName} {"{get;set;}"}";
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static string GetColumnName(string columns, string columnName)
        {
            var name = columnName.Clone().ToString();
            if (!string.IsNullOrWhiteSpace(columns))
            {
                var comments = Regex.Replace(columns, "[A-Za-z0-9]$", "", RegexOptions.IgnoreCase);
                if (!string.IsNullOrWhiteSpace(comments))
                {
                    var pinYinList = MicrosoftPinYinHelper.GetAllPinYin(comments); //NPinyinHelper.GetAllPinYin(comments);
                    if (pinYinList.Any())
                    {
                        columnName = ConvertColumnName(columnName, pinYinList);
                    }
                    if (name == columnName)
                    {
                        pinYinList = NPinyinHelper.GetAllPinYin(comments);
                        if (pinYinList.Any())
                        {
                            columnName = ConvertColumnName(columnName, pinYinList);
                        }
                    }
                }
            }
            return columnName;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetTableName(string columns, string tableName)
        {
            var tableNameList = tableName.Split("_");
            var count = tableNameList.Count();
            tableName = tableNameList.LastOrDefault();
            tableNameList[count - 1] = GetColumnName(columns, tableName);
            return string.Join('_', tableNameList);
        }

        /// <summary>
        /// 处理表名
        /// </summary>
        /// <param name="dto"></param>
        private static void SetTableName(TableDto dto)
        {
            var tableNameList = dto.TableName.Split("_");
            var count = tableNameList.Count();
            var tableName = tableNameList.LastOrDefault();
            var fileName = GetColumnName(dto.Comments, tableName);
            tableNameList[count - 1] = fileName;
            dto.ModelName = string.Join('_', tableNameList);
            dto.FileName = fileName;
            //return string.Join('_', tableNameList);
        }

        /// <summary>
        /// 转换字段名称
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="pinYinList"></param>
        /// <returns></returns>
        private static string ConvertColumnName(string columnName, List<string> pinYinList)
        {
            var count = pinYinList.Count();
            var i = 0;
            var final = false;
            while (i < count - 1 && !final)
            {
                var temp = columnName;
                //匹配第一个拼音
                if (!string.IsNullOrWhiteSpace(pinYinList[i]) && columnName.Contains(pinYinList[i]))
                {
                    var indexOf = temp.IndexOf(pinYinList[i]);
                    if (indexOf == 0)
                    {
                        temp = temp.Substring(pinYinList[i].Length, temp.Length - indexOf - pinYinList[i].Length);
                        //匹配第二个拼音
                        indexOf = temp.IndexOf(pinYinList[i + 1]);
                        if (indexOf == 0)
                        {
                            temp = temp.Substring(pinYinList[i + 1].Length, temp.Length - indexOf - pinYinList[i + 1].Length);
                            //如果只有两个字
                            if (string.IsNullOrWhiteSpace(temp))
                            {
                                columnName = FirstCharToUpper(pinYinList[i]) + FirstCharToUpper(pinYinList[i + 1]);
                                final = true;
                            }
                            else
                            {
                                //如果三个汉字
                                var last = Regex.Replace(temp, "[0-9]", "", RegexOptions.IgnoreCase);
                                if (i + 2 <= count - 1 && last == pinYinList[i + 2])
                                {
                                    columnName = FirstCharToUpper(pinYinList[i]) + FirstCharToUpper(pinYinList[i + 1]) + FirstCharToUpper(pinYinList[i + 2]) + Regex.Replace(temp, "[" + last + "]", "", RegexOptions.IgnoreCase);
                                    final = true;
                                }
                                else//否则前面两个首字母大写
                                {
                                    var hanZiList = new List<string>() { pinYinList[i], pinYinList[i + 1] };
                                    foreach (var item in hanZiList.OrderByDescending(x => x.Length))
                                    {
                                        columnName = columnName.Replace(item, FirstCharToUpper(item));
                                    }
                                    //columnName = columnName.Replace(pinYinList[i], FirstCharToUpper(pinYinList[i])).Replace(pinYinList[i + 1], FirstCharToUpper(pinYinList[i + 1]));
                                    final = true;
                                }
                            }
                        }
                        i++;
                    }
                }
                i++;
            }
            return columnName;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            return str;
        }
        #endregion
    }
}