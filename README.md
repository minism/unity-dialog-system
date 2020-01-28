## Pixel-perfect text setup

This is still hard to do reliably.

Use TMP FontAssetCreator with these settings:
  - Padding: 10
  - Atlas resolution: 4096x4096
  - Render mode: RASTER_HINTED

Then, adjust the font size of the Dialog Box prefab until it looks pixel-perfect on the game view.
I've had success with values near 32.
