# Visual Effect Graph Logic
The Visual Effect Graph uses two distinct workflows: a _processing_ logic and a _property_ logic. Each logic follows general behavior rules. This means that the Visual Effect Graph is both vertical and horizontal. 
## Processing workflow (vertical logic)
The processing workflow links together a succession of customizable stages to define the complete system logic. This is where you can determine when the spawn, initialization, update, and rendering of the particles in the effect.

The processing logic defines the different stages of processing of a visual effect. They are defined by large colored containers called [Contexts]. Each context connects to another compatible context, which defines how the current context is used for the next stage of processing.

You can add one or many Blocks to any context. Every block is a stackable node that is in charge of one operation. You can reorder blocks to change the order in which things happen. 
## Property workflow (horizontal logic)
In the horizontal property workflow, you can enhance the processing workflow by defining from simple to highly technical math operations. This affects how the particles look.

The Visual Effect Graph comes with a block library that is ready to use. The horizontal flow controls the render pipeline passes data to the Blocks and Contexts through a network of connected Nodes.


You can customize how particles behave by connecting horizontal nodes to a block and creating a custom expression. 
To create a custom expression from nodes, you can add nodes through the Add Node Context Menu, connect them to block properties and change their values in order to define the behavior you expect.
## Graph Elements

![The vertical workflow contains Systems, which then contain Contexts, which then contain Blocks. Together, they determine when something happens during the “lifecycle” of the visual effect.](Images/SystemVisual.png)

### Systems
Systems are the main components of a visual effect. Every system defines one distinct part that the render pipeline simulates and renders alongside other systems. In the graph, systems appear as dashed outlines that connect Contexts (see image above).

Generally, a Particle System is composed of a succession of an Initialize, then Update, then Output context. If you don’t want any simulation to happen, but just want to render your particles, you can skip the __Update__ context and directly connect the Initialize context to the Output Context.

If you want to perform multi-pass simulation, you can chain together multiple __Update__ contexts. 

Finally, in some other cases, multiple outputs can be connected to the same simulation in order to compose the rendering of one particle (eg: butterfly particles made of two quads for the wings and a particle mesh for the body)
### Contexts
Contexts are parts of the Systems that define one stage of processing. 

Here are the 4 common contexts in a Visual Effect Graph graph:

* **Spawn**. If active, Unity calls this every Frame, and computes the amount of particles to spawn.
* **Initialize**. Unity calls this at the “birth” of every particle, This defines the initial state of the particle. 
* **Update**. Unity calls this every frame for all particles, and uses this to perform simulations, for example Forces and Collisions.  
* **Output**. Unity calls this every frame for every particle. This determines the shape of a particle, and performs pre-render transformations.

**Note:** Some context, for example Output Mesh, do not connect to any other contexts as they do not relate to other systems.

### Blocks
Blocks are nodes that you can stack into a Context. Every Block is in charge of one operation. For example, it can apply a force to the velocity, collide with a sphere, or set a random color.

To configure and customize Blocks, you can:


* Adjust their Properties by connecting each property’s Port to another Node with an Edge. 
* Adjusting the Settings for a property. Settings are editable values without ports that you cannot connect to other nodes.
### Node Operators
Node operators are low-level operations of the property workflow that you can connect to generate custom behaviors. Node networks connect to Ports that belong to Blocks or Contexts.

### Groups 

In addition to nodes, you can tidy up your graphs by creating groups of nodes that you can drag around and give a title in order to explain what this group does. To add a Group, select some nodes, then use the right-click context menu to select Group Nodes.

### Sticky Notes

Sticky Notes are draggable comment elements you can add to leave explanations and reminders to your co-workers or for yourself.

### Adding Graph Elements

You can add graph elements using various methods depending on what you need to do:

* **Right Click Menu** : Using the right click menu, select Add Node, then select the Node you want to add from the menu. This action is context-sensitive, based on the element that stands below your cursor and will provide you only with the graph elements that are compatible.

* **Spacebar Menu** : This shortcut is the equivalent of making a right-click, then selecting Add Node.

* **Interactive Connections** : While creating an edge from a port (either property or workflow), drag the edge around and release the click into an empty space to display the Node Menu. This action is context-sensitive and will provide you only the compatible graph elements that you can connect to.