<div style="border: solid 1px #999; border-radius:12px; background-color:#EEE; padding: 8px; padding-left:14px; color: #555; font-size:14px;"><b>Draft:</b> This page has its content written, but its formatting has not been reviewed at the moment.</div>
# Property Binders

Property Binders are C# Behaviors you can attach to a GameObject with a [Visual Effect Component](VisualEffectComponent.md) : these behaviors enable making connections between scene or gameplay values and [Exposed Properties](Blackboard.md#exposed-properties-in-inspector) for this Visual Effect instance.

> For instance : a Sphere Binder can automatically set the position and the radius of a Sphere Exposed Property with the values of a sphere collider that is linked in the scene.

## Adding Property Binders

![](Images/PropertyBinder.png)

You can add Property Binders through a common MonoBehaviour called **VFX Property Binder**. This behavior enables the use of one or many **Property Bindings**. Each property binding is in charge of creating a relationship between an [Exposed Property](Blackboard.md#exposed-properties-in-inspector) and a runtime or scene element. 

## Built-in Property Binders

Visual Effect Graph package comes with the following built-in property binders:



## Writing Property Binders

You can write property binders by adding new C# classes to your project.

