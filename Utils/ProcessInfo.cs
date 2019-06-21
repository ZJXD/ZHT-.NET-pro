using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ProcessInfo
    {
        /// <summary>
        /// 进程标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 进程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 进程处理时间
        /// </summary>
        public double ProcessorTime { get; set; }

        /// <summary>
        /// 关联进程物理内存使用量
        /// </summary>
        public long WorkingSet { get; set; }

        /// <summary>
        /// 关联进程分配的物理内存量
        /// </summary>
        public long WorkingSet64 { get; set; }

        /// <summary>
        /// 关联进程主模块完整路径
        /// </summary>
        public string MainModulePath { get; set; }

        public ProcessInfo(int id, string name, double time, long set, string path)
        {
            this.Id = id;
            this.Name = name;
            this.ProcessorTime = time;
            this.WorkingSet = set;
            this.MainModulePath = path;
        }
    }
}
