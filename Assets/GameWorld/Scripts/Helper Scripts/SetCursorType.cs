using UnityEngine;

namespace Methodyca.Core
{
  // This script is for event trigger areas. Since methods with enum parameters aren't accepted,
  // this script will make the event trigger SetCursorImage() method while the enum value
  // is set in cursorType field.
  public class SetCursorType : MonoBehaviour
  {
      public CursorTypes cursorType;

      public void SetCursorImage()
      {
          CursorManager.instance.SetCursor(cursorType);
      }
  }
}