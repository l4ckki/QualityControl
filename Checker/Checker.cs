using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Использование: checker.exe tests.txt triangle.exe result.txt");
            return;
        }

        string testFile = args[0];
        string triangleExe = args[1];
        string resultFile = args[2];

        string[] lines = File.ReadAllLines(testFile);
        StreamWriter writer = new StreamWriter(resultFile);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (line.Trim() == "")
                continue;

            bool result = CheckOneTest(line, triangleExe);

            if (result)
                writer.WriteLine("success;");
            else
                writer.WriteLine("error;");
        }

        writer.Close();
        Console.WriteLine("Проверка завершена.");
    }

    static bool CheckOneTest(string line, string triangleExe)
    {
        string[] parts = line.Split(' ');

        if (parts.Length < 2)
            return false;

        int expectedStart = 1;
        for (int i = parts.Length - 1; i >= 0; i--)
        {
            if (parts[i] == "обычный" || parts[i] == "равнобедренный" || parts[i] == "равносторонний" || parts[i] == "не" || parts[i] == "неизвестная")
            {
                expectedStart = i;
                break;
            }
        }

        string expected = parts[expectedStart];

        for (int i = expectedStart + 1; i < parts.Length; i++)
            expected += " " + parts[i];

        string arguments = "";

        for (int i = 0; i < expectedStart; i++)
            arguments += parts[i] + " ";

        arguments = arguments.Trim();

        string actual = RunTriangle(triangleExe, arguments);

        return actual == expected;
    }

    static string RunTriangle(string exePath, string arguments)
    {
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = exePath;
        info.Arguments = arguments;
        info.RedirectStandardOutput = true;
        info.UseShellExecute = false;
        info.CreateNoWindow = true;

        Process process = Process.Start(info);
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return output.Trim();
    }
}
