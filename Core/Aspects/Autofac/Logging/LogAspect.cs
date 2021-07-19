using System;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerType)
        {
            if (loggerType.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerType);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (!_loggerServiceBase.IsInfoEnabled)
            {
                return;
            }

            var logParameters = invocation.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name,
                Type = t.ParameterType.Name,
                Value = invocation.Arguments.GetValue(i)
            }).ToList();

            var logDetail = new LogDetail
            {
                FullName = invocation.Method.DeclaringType == null ? null : invocation.Method.DeclaringType.FullName,
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            _loggerServiceBase.Info(logDetail);
        }
    }
}
