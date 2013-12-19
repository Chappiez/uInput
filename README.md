uInput
======

uInput is an alternative to the default Input Manager bundled with Unity3D. It allows you to define and modify keyboard inputs and axis at runtime, which is something the default Input Manager does not, surprisingly. It's purely script-driven so don't expect any GUI.

Usage is quite simple : use `uInput.Init()` as soon as possible in your code, and call `uInput.Update()` on every frame. You can skip this last step if you don't make use of axes.

Key bindings can be defined as follows :

    uInput.DefineKey("Jump", KeyCode.X);
    uInput.DefineKey("Fire", KeyCode.C);
    uInput.DefineAxis("Horizontal", KeyCode.LeftArrow, KeyCode.RightArrow, 0.1f, false);

And then used as follows :

    if (uInput.IsPressed("Jump"))
        Jump();
	   
    if (uInput.IsDown("Fire"))
        Fire();
    
    float speedX = uInput.GetAxis("Horizontal") * speed;

See the in-code documentation for more information about every method and parameter.

Note : at the moment, µInput only handles keyboard events. For mouse and joystick, you'll have to stick to the default Input Manager.

License
-------

MIT (see [License.txt](LICENSE.txt))
