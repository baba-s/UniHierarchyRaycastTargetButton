using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane.Internal
{
	/// <summary>
	/// Hierarchy に Raycast Target を変更できるボタンを追加するエディタ拡張
	/// </summary>
	internal static class HierarchyRaycastTargetButton
	{
		//================================================================================
		// 定数
		//================================================================================
		private const int WIDTH = 16;

		//================================================================================
		// 変数(static)
		//================================================================================
		private static Texture m_image;

		//================================================================================
		// プロパティ(static)
		//================================================================================
		// ボタンで表示するテクスチャを返します
		private static Texture Image
		{
			get
			{
				if ( m_image == null )
				{
					var type    = typeof( GraphicRaycaster );
					var content = EditorGUIUtility.ObjectContent( null, type );

					m_image = content.image;
				}

				return m_image;
			}
		}

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		[InitializeOnLoadMethod]
		private static void Example()
		{
			EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
		}

		/// <summary>
		/// GUI を描画する時に呼び出されます
		/// </summary>
		private static void OnGUI( int instanceID, Rect selectionRect )
		{
			var settings = HierarchyRaycastTargetButtonSettings.Instance;

			if ( settings == null || !settings.IsEnable ) return;

			var gameObject = EditorUtility.InstanceIDToObject( instanceID ) as GameObject;

			if ( gameObject == null ) return;

			var graphic = gameObject.GetComponent<Graphic>();

			if ( graphic == null ) return;

			var position = selectionRect;

			position.x     = position.xMax - WIDTH;
			position.width = WIDTH;

			var oldColor      = GUI.color;
			var raycastTarget = graphic.raycastTarget;

			GUI.color = raycastTarget ? Color.white : Color.gray;

			if ( GUI.Button( position, Image, GUIStyle.none ) )
			{
				Undo.RecordObject( graphic, "Inspector" );
				graphic.raycastTarget = !raycastTarget;

				// UniSceneViewRaycastTargetVisualizer の描画を更新するためには
				// EditorUtility.SetDirty を呼び出す必要がある
				EditorUtility.SetDirty( graphic );
			}

			GUI.color = oldColor;
		}
	}
}