using System;

namespace TestingFramework.Commons
{
    public static class EmailGenerationHelper
    {
        public static Random random = new Random();

        public static int NumForEmail => random.Next(999999);

        public static string MailBoxName => $"petrov{NumForEmail}";
    }
}
