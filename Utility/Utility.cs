using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionDB
{
	internal class Utility
	{
		public static async Task<bool> CheckPermissions()
		{
			var readPermission = await Permissions.RequestAsync<Permissions.StorageRead>();
			var writePermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
			if (readPermission != PermissionStatus.Granted || writePermission != PermissionStatus.Granted)
				return false;
			else
				return true;
		}
	}
}
