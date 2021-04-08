using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kpi.YourDomain.ClientTests.Platform.Waiter
{
    public static class WaitFor
    {
        public static void Condition(Func<bool> waitCondition, string timeoutMessage,
            ExceptionsDuringWait ignoreExceptionsDuringWait = ExceptionsDuringWait.Ignore)
        {
            Condition(waitCondition, timeoutMessage, TimeSpan.FromSeconds(30), ignoreExceptionsDuringWait);
        }

        public static void Condition(Func<bool> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            TimeSpan timeStep = default)
        {
            Condition(waitCondition, timeoutMessage, maxWaitTime, ExceptionsDuringWait.Ignore, timeStep);
        }

        /// <summary>
        /// Wait for the passed function to return true. Throws exception when passed function has returned false on every call
        /// </summary>
        /// <param name="waitCondition">Function that is expected to return true before timeout. Upon retrieving true the waiter returns.</param>
        /// <param name="timeoutMessage">Verbose error message that explains why the function never returned true.</param>
        /// <param name="maxWaitTime">Maximum timeout. When it's reached, an error with the timeout message is thrown.</param>
        /// <param name="timeStep">Timeout between retries.</param>
        public static async Task Condition(Func<Task<bool>> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            TimeSpan timeStep = default)
        {
            await Condition(waitCondition, timeoutMessage, maxWaitTime, ExceptionsDuringWait.Ignore, timeStep);
        }

        private static void Condition(Func<bool> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            ExceptionsDuringWait ignoreExceptionsDuringWait, TimeSpan timeStep = default)
        {
            var exceptionsDuringWait = new StringBuilder();
            var stopwatch = Stopwatch.StartNew();
            TimeSpan step;
            if (timeStep == default)
            {
                // calculate  1/10 of max wait time  
                // calculate sleep step. It will be smaller of
                // - 10 seconds
                // - 1/10 maxWaitTime interval
                step = TimeSpan.FromMilliseconds(maxWaitTime.TotalMilliseconds / 20);
                step = step > TimeSpan.FromSeconds(10) ? TimeSpan.FromSeconds(10) : step;
            }
            else
            {
                step = timeStep;
            }

            // set checks count
            var checksDone = 0;

            // try till max time elapsed
            while (stopwatch.Elapsed < maxWaitTime || checksDone == 0)
            {
                try
                {
                    if (waitCondition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect)
                    {
                        // if an exception occurred , save exception Message
                        exceptionsDuringWait.AppendLine(e.Message);
                    }

                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace)
                    {
                        // if an exception occurred , save exception Message
                        exceptionsDuringWait.AppendLine(e.ToString());
                    }
                }

                // increase number of checks performed
                checksDone++;

                // wait some time before second attempt
                Thread.Sleep(step);
            }

            var exceptionMsg = $"Timeout after {maxWaitTime.TotalSeconds} seconds: {timeoutMessage}";
            if ((ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect ||
                 ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace) &&
                string.IsNullOrEmpty(exceptionsDuringWait.ToString()))
            {
                throw new Exception(exceptionMsg);
            }

            throw new Exception($"{exceptionMsg} . Exceptions During Wait:( {exceptionsDuringWait} )");
        }

        private static async Task Condition(Func<Task<bool>> waitCondition, string timeoutMessage, TimeSpan maxWaitTime,
            ExceptionsDuringWait ignoreExceptionsDuringWait, TimeSpan timeStep = default)
        {
            StringBuilder exceptionsDuringWait = new StringBuilder();
            Stopwatch stopwatch = Stopwatch.StartNew();
            TimeSpan step;
            if (timeStep == default)
            {
                step = TimeSpan.FromMilliseconds(maxWaitTime.TotalMilliseconds / 20);
                step = step > TimeSpan.FromSeconds(10) ? TimeSpan.FromSeconds(10) : step;
            }
            else
            {
                step = timeStep;
            }

            int checksDone = 0;

            while (stopwatch.Elapsed < maxWaitTime || checksDone == 0)
            {
                try
                {
                    if (await waitCondition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect)
                    {
                        exceptionsDuringWait.AppendLine(e.Message);
                    }

                    if (ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace)
                    {
                        exceptionsDuringWait.AppendLine(e.ToString());
                    }
                }

                checksDone++;
                await Task.Delay(step);
            }

            string exceptionMsg = $"Timeout after {maxWaitTime.TotalSeconds} seconds: {timeoutMessage}";
            if ((ignoreExceptionsDuringWait == ExceptionsDuringWait.Collect
                 || ignoreExceptionsDuringWait == ExceptionsDuringWait.CollectWithStackTrace)
                && !string.IsNullOrEmpty(exceptionsDuringWait.ToString()))
            {
                throw new Exception($"{exceptionMsg} . Exceptions During Wait:( {exceptionsDuringWait} )");
            }

            throw new Exception(exceptionMsg);
        }
    }
}
