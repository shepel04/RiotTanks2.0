using System;

namespace DefaultNamespace
{
    public class UniqueIdGenerator
    {
        public static string GenerateUniqueId()
        {
            string uniqueId = Guid.NewGuid().ToString();
            return uniqueId;
        }
    }
}