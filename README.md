# TouchButton
Handy script for creating touch buttons with ability to auto-generate animator controllers.

Use this script for super quick touch controls implementations while prototyping or doing your own project.

__Step 1__  
  Right click on any element inside canvas and select UI/Touch Button:  
  ![img1](http://rebound.studio/create_touchButton.png)  
  This will create a default touch button with image component, and place it as child under the gameobject you clicked on.
__Step 2.__  
  Select newly created object:  
  ![img2](http://rebound.studio/generate_promt.png "Press button!")  
  Hitting the "Generate Animator" button will present dialogue window, where you must select a location of animator
  controller and it's name. The default path is set your Unity project.
  _Please note that animator controller must be saved inside {project-name}/Assets/.. folder._<br />
__Step 3__  
  The newly created animator controller should look like this:  
  ![img3](http://rebound.studio/generate_promt.png "Receive bacon!")  
  Animator state machine is already configured with necessary transitions and triggers:  
  ![img4](http://rebound.studio/controllerView.png)  
  
And that's all! It's a properly configured touch button. You can start editing the animation clips as normal.

__Important__
  When editing __Release__ animation clip, do note the existance of __event__ at the start of this clip.  
  ![img5](http://rebound.studio/animationEvent.png)  
  This event invokes the UnityEvent on the button gameobject. When you finish editing the animation clip, move it to the
  last frame of animation.
  
Please drop me a message if you have any suggestions.
