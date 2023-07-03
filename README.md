# AR-Kit-Animation-Remote-Control
Remotely control mobile displayed AR animation or object with REST API.


### System Overview
![ex_screenshot](https://github.com/jinwook31/AR-Kit-Animation-Remote-Control/blob/main/img/overall.png)

### Setup
Install and Set-up the Python REST API server on the Desktop and execute it in the background.
Also, make Unity ARKit project (install package - https://www.youtube.com/watch?v=iRxDKCc6Z64&list=PLQMQNmwN3FvzCWfvCvq2AYh1CFnTlv2Es)
Then use the scripts provided in this repo.


### Animation Controller (Tkinter)
If you open the animation controller, this GUI will pop up.
Chapter changes the animations and the Display guide does nothing yet.
Still, it delivers both values to the ARKit app! (You can modify it yourself!).
'초기화' is the button for re-init.


![ex_screenshot](https://github.com/jinwook31/AR-Kit-Animation-Remote-Control/blob/main/img/manager.PNG)


### ARKit (Unity)
In the script SpawnObjectOnPlane.cs, Place the objects in the placable object lists.
This number is the same as the number in Animation Controller.

![ex_screenshot](https://github.com/jinwook31/AR-Kit-Animation-Remote-Control/blob/main/img/inspector.png)


### Demo Video
link in the process... (please wait!)

