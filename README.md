# PersonalKeymap
You can set keymap as desired.

## Tutorial
1. import this package to unity.
2. open setting window in menu > Keymaps > Setting.
3. Click 'Demo', so you can load demo json. 
4. So, You can set keymap as desired.
5. Click 'Save' you can use keymap.

## Example
### Setting keymap (Demo)
* Jump: Input.GetKeyDown(KeyCode.Space)
* Front: Input.GetKey(KeyCode.UpArrow)
* Attack: Input.GetKeyDown(KeyCode.Return)
* SpecialAttack: Input.GetKey(KeyCode.F) && Input.GetKeyDown(KeyCode.J)

### Your Input class (In Demo.cs)
```cs
[Serializable]
public class Mapping
{
	public Keymap Jump;
	public Keymap Front;
	public Keymap Attack;
	public Keymap SpecialAttack;
}
```

