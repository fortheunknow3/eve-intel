using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EVEAntiCollision.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.2.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("200")]
		public int start_x
		{
			get
			{
				return (int)this["start_x"];
			}
			set
			{
				this["start_x"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("300")]
		public int start_y
		{
			get
			{
				return (int)this["start_y"];
			}
			set
			{
				this["start_y"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("100")]
		public int area_x
		{
			get
			{
				return (int)this["area_x"];
			}
			set
			{
				this["area_x"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("700")]
		public int area_y
		{
			get
			{
				return (int)this["area_y"];
			}
			set
			{
				this["area_y"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool ContinuousReminder
		{
			get
			{
				return (bool)this["ContinuousReminder"];
			}
			set
			{
				this["ContinuousReminder"] = value;
			}
		}
	}
}
