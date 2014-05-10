using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectToMidi.Models;
using Microsoft.Kinect;
using NUnit.Framework;
using Rhino.Mocks;

namespace KinectToMidi.Models.Tests
{
    [TestFixture()]
    public class ConditionsBlockTests
    {
        [Test()]
        public void HandleConditions_GoIntoAndSignalEventTypeIn_ExpectSend()
        {
            var skeleton = new Skeleton();

            var midiMock = MockRepository.GenerateMock<BaseMidiSignal>();

            var conditionsBlock = new ConditionsBlock();
            midiMock.SignalEventType = SignalEventTypes.In;
            //makes int behavior
            conditionsBlock.MidiSignals.Add(midiMock);
            conditionsBlock.Conditions.Add(new CuboidCondition()
            {
                Cuboid = Helper.MakeCuboidAroundZero(),
                JointIndex = Helper.GetRightHandIndex()
            });

            midiMock.Expect(x => x.Send(skeleton));
            conditionsBlock.HandleConditions(skeleton);
            midiMock.VerifyAllExpectations();
        }

        [Test()]
        public void HandleConditions_GoIntoAndSignalEventTypeOver_ExpectSend()
        {
            var skeleton = new Skeleton();

            var midiMock = MockRepository.GenerateMock<BaseMidiSignal>();

            var conditionsBlock = new ConditionsBlock();
            midiMock.SignalEventType = SignalEventTypes.Over;
            conditionsBlock.MidiSignals.Add(midiMock);
            //makes over behavior
            conditionsBlock.Conditions.Add(new CuboidCondition()
            {
                Cuboid = Helper.MakeCuboidAroundZero(),
                JointIndex = Helper.GetRightHandIndex()
            });
            conditionsBlock.HandleConditions(skeleton);

            midiMock.Expect(x => x.Send(skeleton));
            conditionsBlock.HandleConditions(skeleton);
            midiMock.VerifyAllExpectations();
        }

        [Test()]
        public void HandleConditions_GoOutAndSignalEventTypeOut_ExpectSend()
        {
            var skeleton = new Skeleton();

            var midiMock = MockRepository.GenerateMock<BaseMidiSignal>();

            var conditionsBlock = new ConditionsBlock();
            midiMock.SignalEventType = SignalEventTypes.Out;
            conditionsBlock.MidiSignals.Add(midiMock);
            //makes out behavior
            conditionsBlock.Conditions.Add(new CuboidCondition()
            {
                Cuboid = Helper.MakeCuboidAroundZero(),
                JointIndex = Helper.GetRightHandIndex()
            });
            conditionsBlock.HandleConditions(skeleton);
            ((CuboidCondition)conditionsBlock.Conditions[0]).Cuboid = Helper.MakeCuboidWithNoZero();

            midiMock.Expect(x => x.Send(skeleton));
            conditionsBlock.HandleConditions(skeleton);
            midiMock.VerifyAllExpectations();
        }
    }
}
