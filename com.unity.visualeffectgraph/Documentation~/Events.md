# Events

Events are the Processing Workflow inputs of a Visual Effect Graph. Through Events, a Visual Effect can :

* Start and stop spawning particles, 
* Read Attribute payloads sent from C#

Events are used in the graph as inputs for Spawn Contexts and Initialize

## Creating Events

![](Images/EventContexts.png)

You can Create Events using Event Contexts. These contexts have no Flow input and connect to Spawn or Initialize Contexts.

In order to Create an Event Context, right click in an empty space of the Workspace and select Create Node, then Select **Event (Context)** from the Node Creation menu.

## Default Events

Visual Effect Graphs provide two Default Events that are implicitly bound to the Start and Stop Flow Inputs of the Spawn Contexts:

* `OnPlay` for the intent *Enabling the Spawn of Particles*, is implicitly bound to the Start Flow input of any Spawn Context.
* `OnStop` for the intent of *Stopping the Spawn of Particles*, is implicitly bound to the Stop Flow input of any Spawn Context.

Connecting Event Contexts on the Start and Stop Flow inputs of a Spawn Contexts will remove the implicit binding to the `OnPlay` and `OnStop` Events

## Custom Events

Custom Events can be created inside Visual Effect Graphs using Event Contexts.

Creating

## EventAttribute Payloads

## Default VisualEffect Event



