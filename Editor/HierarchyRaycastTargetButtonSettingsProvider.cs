using UnityEditor;

namespace Kogane.Internal
{
	/// <summary>
	/// Preferences における設定画面を管理するクラス
	/// </summary>
	internal sealed class HierarchyRaycastTargetButtonSettingsProvider : SettingsProvider
	{
		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HierarchyRaycastTargetButtonSettingsProvider( string path, SettingsScope scope )
			: base( path, scope )
		{
		}

		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		public override void OnGUI( string searchContext )
		{
			var settings = HierarchyRaycastTargetButtonSettings.Instance;

			using ( var checkScope = new EditorGUI.ChangeCheckScope() )
			{
				settings.IsEnable = EditorGUILayout.Toggle( "Enabled", settings.IsEnable );

				if ( checkScope.changed )
				{
					settings.SaveToEditorPrefs();
				}
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// Preferences にメニューを追加します
		/// </summary>
		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path     = "Preferences/UniHierarchyRaycastTargetButton";
			var provider = new HierarchyRaycastTargetButtonSettingsProvider( path, SettingsScope.User );

			return provider;
		}
	}
}