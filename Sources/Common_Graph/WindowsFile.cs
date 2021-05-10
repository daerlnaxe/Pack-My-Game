using Hermes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace CommonGraph
{
    public class WindowsFile
    {
        public bool CheckWriteFolderAccess(string path)
        {

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectorySecurity acl = di.GetAccessControl();
                AuthorizationRuleCollection rules = acl.GetAccessRules(true, true, typeof(NTAccount));

                WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
                WindowsPrincipal group = new WindowsPrincipal(currentUser);

                foreach (AuthorizationRule rule in rules)
                {
                    FileSystemAccessRule fsAccessRule = rule as FileSystemAccessRule;
                    if (fsAccessRule == null)
                        continue;

                    if ((fsAccessRule.FileSystemRights & FileSystemRights.WriteData) > 0)
                    {
                        NTAccount ntAccount = rule.IdentityReference as NTAccount;
                        if (ntAccount == null)
                        {
                            continue;
                        }

                        if (group.IsInRole(ntAccount.Value))
                        {
                            HeTrace.WriteLine($"Current user is in role of {ntAccount.Value}, has write access");
                            continue;
                        }
                        HeTrace.WriteLine($"Current user is not in role of {ntAccount.Value}, does not write access");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                HeTrace.WriteLine("does not have full access");
            }
        }
    }
}
