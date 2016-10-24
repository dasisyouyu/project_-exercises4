using Himy.Models.Logs;
using Himy.Models.Master;
using System;

namespace Himy.Models
{
    /// <summary>
    /// 例外、デバッグ内容をDBへ出力する機能を実装したクラスです。
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// ログ種別とメッセージを指定し、ログを出力します。
        /// </summary>
        /// <param name="logTypeId"></param>
        /// <param name="message"></param>
        public void WriteLog(MstLogTypes logTypeId, string message)
        {
            using (HContext context = new HContext())
            {
                Log log = new Log();
                log.LogTypeId = logTypeId;
                log.Message = message;
                log.DateCreated = DateTime.Now;

                context.Logs.Add(log);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Exceptionを指定し、ログを出力します。
        /// </summary>
        /// <param name="logTypeId"></param>
        /// <param name="message"></param>
        public void WriteLog(Exception e)
        {
            using (HContext context = new HContext())
            {
                Log log = new Log();
                log.LogTypeId = MstLogTypes.Error;
                log.Message = e.Message;
                log.Contents = e.StackTrace;
                log.DateCreated = DateTime.Now;

                context.Logs.Add(log);
                context.SaveChanges();

                if (e.InnerException != null)
                {
                    Log innerLog = new Log();
                    innerLog.LogTypeId = MstLogTypes.Error;
                    innerLog.Message = e.InnerException.Message;
                    innerLog.Contents = e.InnerException.StackTrace;
                    innerLog.DateCreated = DateTime.Now;

                    context.Logs.Add(innerLog);
                    context.SaveChanges();
                }
            }
        }
    }
}
