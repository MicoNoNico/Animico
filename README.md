# Animico
Animico is a simple tweening library for Unity created for personal purposes.
<br>
<br>
<br>

## Currently available methods:
<br>

Anix, AniY, Aniz
  - animate objects or UI elements positions on the specified axis

AniScaleX, AniScaleY, AniScaleZ, AniScale
  - scale objects or UI elements on the specified axis or on all of them.

AniColorImage, AniAlpha
  - change the color or alpha of an Image component.

AniColorMaterial, AniAlphaMaterial
  - change the color or alpha of a Material.
<br>
<br>

## How to use
Download the folder NC.Animico and place it anywhere in your Unity assets.

Remember to type `using NC.Animico` on the script you wish to use the animations in.
<br>
<br>

## Example usages
```
Animico.AniX(targetObject, endPositionX, duration);
```

You can use the provided ease types:
Linear, Ease In, Ease Out, Ease In Out
```
Animico.AniX(targetObject, endPositionX, duration, Animico.EaseInOut);
```

The library supports onComplete callbacks:
```
Animico.AniX(targetObject, endPositionX, duration, Animico.EaseInOut, () => {
  Debug.Log("Animation finished");
});
```
```
Animico.AniX(targetObject, endPositionX, duration, Animico.EaseInOut, () => {
  Animico.AniY(targetObject, endPositionY, duration);
});
```
