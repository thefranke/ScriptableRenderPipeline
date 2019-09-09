# Systems

Systems are compounds of one or many  [Contexts](Contexts.md) that define a standalone part of a Visual Effect. A system can be a Particle System, a Particle Strip System, a Mesh, or a Spawn machine.

<u>Systems can interact between themselves among a Visual Effect Graph :</u> 

* A **Spawn** System can **spawn particles** in one or many Particle or Systems : This is the main method of spawning particles.

* **Particle Systems** can **spawn particles** in **other particle systems** through GPU Events : This alternate method can spawn particles from other particles based on simulation events (for example : death of a particle).

* A **Spawn** System can **Turn on/off** other **Spawn Systems** : This enables synchronizing emission by using a master Spawn System to orchestrate other Spawn Systems.

  

# Creating System from Templates

Visual Effect Graph comes with Built-in templates that you can add to your graph using the following:

1.  Right Click in an empty space of your workspace, then select Create Node
2. In The Node Creation Menu, Select **System** Category
3. Select a template from the system list to add a template system.

![](Images/SystemAddTemplate.png)

