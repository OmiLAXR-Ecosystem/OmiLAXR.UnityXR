using System.Collections.Generic;
using NUnit.Framework;
using OmiLAXR.Pipelines;
using OmiLAXR.TrackingBehaviours.Learner;
using OmiLAXR.UnityXR.Listeners;
using OmiLAXR.UnityXR.TrackingBehaviours;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace OmiLAXR.UnityXR.Tests
{
    public class UnityXR_Tests
    {
        public class TestableInteractableTrackingBehaviour : XRBaseInteractableTrackingBehaviour
        {
            public void Feed(params Object[] objects) => AfterFilteredObjects(objects);
            public void DisposeObjects(params Object[] objects) => Dispose(objects);
        }

        private GameObject _pipelineObject;
        private readonly List<GameObject> _created = new List<GameObject>();

        [SetUp]
        public void SetUp()
        {
            _pipelineObject = new GameObject("Learner Pipeline");
            _pipelineObject.SetActive(false);
            _pipelineObject.AddComponent<LearnerPipeline>();
            _pipelineObject.AddComponent<Actor>();
        }

        [TearDown]
        public void TearDown()
        {
            _pipelineObject.GetComponent<Pipeline>().StopPipeline();
            foreach (var go in _created)
                Object.DestroyImmediate(go);
            _created.Clear();
            Object.DestroyImmediate(_pipelineObject);
        }

        private XRSimpleInteractable CreateInteractable(string name)
        {
            var go = new GameObject(name);
            _created.Add(go);
            return go.AddComponent<XRSimpleInteractable>();
        }

        private TestableInteractableTrackingBehaviour CreateTrackingBehaviour(List<string> calls)
        {
            var tb = _pipelineObject.AddComponent<TestableInteractableTrackingBehaviour>();
            tb.OnTouched.AddHandler((_, e) => calls.Add("touched:" + e.Target.name));
            tb.OnReleased.AddHandler((_, e) => calls.Add("released:" + e.Target.name));
            tb.OnGrabbed.AddHandler((_, e) => calls.Add("grabbed:" + e.Target.name));
            tb.OnInteracted.AddHandler((_, e) => calls.Add("interacted:" + e.Target.name));
            return tb;
        }

        [Test]
        public void XRBaseInteractableTrackingBehaviour_Forwards_Interactable_Events()
        {
            // Arrange
            var calls = new List<string>();
            var tb = CreateTrackingBehaviour(calls);
            var interactable = CreateInteractable("Cube");
            tb.Feed(interactable);

            // Act
            interactable.hoverEntered.Invoke(new HoverEnterEventArgs { interactableObject = interactable });
            interactable.hoverExited.Invoke(new HoverExitEventArgs { interactableObject = interactable });
            interactable.selectEntered.Invoke(new SelectEnterEventArgs { interactableObject = interactable });
            interactable.selectExited.Invoke(new SelectExitEventArgs { interactableObject = interactable });

            // Assert
            CollectionAssert.AreEqual(
                new[] { "touched:Cube", "released:Cube", "grabbed:Cube", "interacted:Cube" }, calls);
        }

        [Test]
        public void XRBaseInteractableTrackingBehaviour_Dispose_Unsubscribes_All_Interactables()
        {
            // Arrange
            var calls = new List<string>();
            var tb = CreateTrackingBehaviour(calls);
            var first = CreateInteractable("First");
            var second = CreateInteractable("Second");
            tb.Feed(first, second);

            // Act
            tb.DisposeObjects(first, second);
            first.hoverEntered.Invoke(new HoverEnterEventArgs { interactableObject = first });
            second.hoverEntered.Invoke(new HoverEnterEventArgs { interactableObject = second });
            first.selectEntered.Invoke(new SelectEnterEventArgs { interactableObject = first });

            // Assert
            CollectionAssert.IsEmpty(calls);
        }

        [Test]
        public void UnityXRLearnerPipelineExtension_Extends_Learner_Pipeline()
        {
            // Arrange
            var pipeline = _pipelineObject.GetComponent<LearnerPipeline>();
            var extensionObject = new GameObject("UnityXR Extension");
            extensionObject.SetActive(false);
            _created.Add(extensionObject);
            var extension = extensionObject.AddComponent<UnityXRLearnerPipelineExtension>();
            var listener = extensionObject.AddComponent<XROriginListener>();

            // Act
            extension.Extend(pipeline);

            // Assert
            Assert.AreEqual(pipeline, extension.Pipeline);
            Assert.Contains(listener, pipeline.Listeners);
        }

        [Test]
        public void XROriginListener_Finds_Origins_In_Scene()
        {
            // Arrange
            var originObject = new GameObject("XR Origin");
            _created.Add(originObject);
            var origin = originObject.AddComponent<XROrigin>();
            var listener = _pipelineObject.AddComponent<XROriginListener>();
            Object[] found = null;
            listener.OnFoundObjects += objects => found = objects;

            // Act
            listener.StartListening();

            // Assert
            CollectionAssert.Contains(found, origin);
        }
    }
}
