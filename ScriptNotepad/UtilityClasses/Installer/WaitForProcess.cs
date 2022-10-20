#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Collections.Generic;
using System.Diagnostics;

/*
 * This files is to be used with the software executable
 * int the entry point of the application to wait for the installer
 * process to exit. I.e. Program.cs:
 * ...Main(...)
 * WaitForProcess.WaitForProcessArguments(args, 30); // wait for 30 seconds..
 */

// ReSharper disable once CheckNamespace
namespace VPKSoft.WaitForProcessUtil;

/// <summary>
/// A class to wait for a process to finish executing with a possibility for a time-out.
/// </summary>
// ReSharper disable once UnusedMember.Global
public class WaitForProcess
{
    /// <summary>
    /// Gets the process identifier from a command line argument array.
    /// </summary>
    /// <param name="args">The arguments to look for a '--waitPid' argument.</param>
    /// <returns>The process identifier to wait for or <c>-1</c> of the argument is not defined or some kind of error occurred.</returns>
    public static int GetWaitProcessId(string[] args)
    {
        try
        {
            List<string> argList = new List<string>(args);
            var index = argList.IndexOf("--waitPid"); // case-sensitive..

            // must have the following process id number..
            if (index != -1 && index + 1 < argList.Count) 
            {
                // check the ar
                if (int.TryParse(argList[index + 1], out _)) 
                {
                    return int.Parse(argList[index + 1]);
                }
            }

            return -1;
        }
        catch
        {
            return -1; // unknown reason of failure in a prefect code (ha-ha)..
        }
    }

    /// <summary>
    /// Waits for a process which identifier is specified in the given arguments with a '--waitPid' argument to exit.
    /// </summary>
    /// <param name="args">The arguments to check for.</param>
    /// <param name="maxWaitSeconds">The maximum amount in seconds to wait for the process to exit.</param>
    // ReSharper disable once UnusedMember.Global
    public static void WaitForProcessArguments(string[] args, int maxWaitSeconds = 0)
    {
        WaitProcess(GetWaitProcessId(args), maxWaitSeconds);
    }

    /// <summary>
    /// Waits the process to exit with a specified process identifier and a specified time-out.
    /// </summary>
    /// <param name="processId">The process identifier for the process which exit to wait for.</param>
    /// <param name="maxWaitSeconds">The maximum amount in seconds to wait for the process to exit.</param>
    public static void WaitProcess(int processId, int maxWaitSeconds = 0)
    {
        if (processId == -1) // invalid identifier = no deal..
        {
            return;
        }

        try
        {
            using (var process = Process.GetProcessById(processId))
            {

                if (maxWaitSeconds == 0)
                {
                    process.WaitForExit();
                }

                if (maxWaitSeconds > 0) // if the wait time is specified, then wait for the process in a loop..
                {
                    var waitCount = 0;
                    var waitMax = maxWaitSeconds * 1000;
                    while (waitCount < waitMax) // ..for the specified maximum time amount..
                    {
                        if (process.WaitForExit(100))
                        {
                            return;
                        }

                        waitCount += 100;
                    }
                }
            }
        }
        catch
        {
            // the process information couldn't be gotten..
        }
    }
}