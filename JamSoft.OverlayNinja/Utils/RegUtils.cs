namespace JamSoft.OverlayNinja.Utils
{
    using Microsoft.Win32;

    public static class RegUtils
    {
        public static bool RenameSubKey(RegistryKey parentKey, string subKeyName, string newSubKeyName)
        {
            var copied = CopyKey(parentKey, subKeyName, newSubKeyName);
            if (copied)
            {
                parentKey.DeleteSubKeyTree(subKeyName);
                return true;
            }

            return false;
        }

        private static bool CopyKey(RegistryKey parentKey, string keyNameToCopy, string newKeyName)
        {
            using (RegistryKey destinationKey = parentKey.CreateSubKey(newKeyName, true))
            {
                using (RegistryKey sourceKey = parentKey.OpenSubKey(keyNameToCopy))
                {
                    RecurseCopyKey(sourceKey, destinationKey);
                }
            }

            return true;
        }

        private static void RecurseCopyKey(RegistryKey sourceKey, RegistryKey destinationKey)
        {
            foreach (string valueName in sourceKey.GetValueNames())
            {
                object objValue = sourceKey.GetValue(valueName);
                RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                destinationKey.SetValue(valueName, objValue, valKind);
            }

            foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                using (RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName))
                {
                    using (RegistryKey destSubKey = destinationKey.CreateSubKey(sourceSubKeyName))
                    {
                        RecurseCopyKey(sourceSubKey, destSubKey);
                    }
                }
            }
        }
    }
}