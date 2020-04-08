# Resource component

A component to represent finite resources, like HP, MP, Stamina, for example.

## Public properties

* Max - The maximum value a resource can achieve
* Value - The current value of a resource, between 0 and Max

## Public methods

    public bool IsEmpty();

Returns if the `Value` of the resource equals zero

    public bool IsFull();

Return if the `Value` of the resource equals `Max`

    public bool HasAtLeast(float leastValue);

Returns if the `Value` of the resource is equals or greater than `leaveValue`

    public float Percentage();

Return the percentage of the resource, in relationship to its `Max`

    public void Fill();

Sets the `Value` of the resource equal to its `Max`

    public void Deplete();

Sets the `Value` of the resource equal to zero

    public void Add(float amount);

Adds the `amount` to the `Value` of the resource

    public void AddModifier(IResourceModifier modifier);

Adds a modifier to the resource (see below to learn more about modifiers).

    public void UpdateOverTime(float frequency, float duration, float amount);

Adds a `ResourceTimeModifier` to the resource (see below to learn more about modifiers).

## Modifiers

Resources might contain many modifiers. Modifiers allow you to change the value of a resource in an automated way.

Let's take a look at `ResourceTimeModifier`. It will allow you to modify a resource in small amounts during periods of time, for a certain amount of time.

Example:
Imagine that your character gets poisoned and you want it to lose 2 points of HP every 5 seconds, for 30 seconds. You would use the a modifier and apply it to the resource:

```cs
Resource hp = player.GetComponent<Resource>();

ResourceTimeModifier poisonModifier = new ResourceTimeModifier(5, -2, 30);

hp.AddModifier(poisonModifier);
```

### Build your own modifiers

You can build your own modifiers, by simply implementing the `IResourceModifier` interface.

Let's say that you want to apply a modifier that increases your HP 10% every second endlessly. You could build this modifier:

```cs
public class CustomModifier : IResourceModifier
{
    public bool HasExpired()
    {
        return false;
    }

    public float Execute(Resource resource, float deltaTime)
    {
        return resource.Value * 0.1f * deltaTime;
    }
}
```
