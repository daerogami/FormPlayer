using QuantumGate.GameObjects.Actors.Concrete;
using QuantumGate.GameObjects.Beats.Concrete;
using QuantumGate.GameObjects.Contents.Concrete;
using QuantumGate.GameObjects.Cues.Concrete;

namespace QuantumGate.GameObjects.Actors.Factories
{
    public class HotspotActorFactory
    {
        public static HotspotActor CreateStateChangeNavigation(NavigationHotspot direction, string destinationStage, Video transitionVideo)
        {
            var newNavigationActor = new HotspotActor { ControlName = direction.ToString() };
            var navigationCue = new OnClickCue();
            navigationCue.Beats.Add(new ChangeStageBeat { DestinationStage = destinationStage, TransitionVideo = transitionVideo});
            newNavigationActor.Cues.Add(navigationCue);

            return newNavigationActor;
        }
    }
}