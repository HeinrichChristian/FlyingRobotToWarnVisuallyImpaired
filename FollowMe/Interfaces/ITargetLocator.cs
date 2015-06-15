using FollowMe.Enums;

namespace FollowMe.Interfaces
{
    public interface ITargetLocator
    {
        TargetLocation GetTargetLocation(bool trackingPreviewEnabled, int searchObjectSizePixels, int hueMin, int hueMax, float saturationMin, float saturationMax, float luminanceMin, float luminanceMax);

        TargetLocation GetGlyphLocation();
    }
}
