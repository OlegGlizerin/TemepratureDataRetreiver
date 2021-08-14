using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Service.Interfaces;
using Service.PubSub;

namespace UnitTests
{
    [TestClass]
    public class AmazonServiceTests
    {
        private IAmazonSubscriber _amazonSubscriber;
        private IAmazonPublisher _amazonPublisher;
        private IAmazonService _amazonService;

        private string _command = "Start";
        private string _message = "1";

        [TestInitialize]
        public void TestInit()
        {
            _amazonSubscriber = A.Fake<IAmazonSubscriber>();
            _amazonPublisher = A.Fake<IAmazonPublisher>();
            _amazonService = new AmazonService(_amazonSubscriber, _amazonPublisher);
        }

        [TestMethod]
        public void AmazonService_TryingToSendEmptyMessage_NotifyWontHappened()
        {
            //Arrange
            var message = "";
            
            //Act
            _amazonService.SendMessage(_command, message);

            //Assert
            A.CallTo(() => _amazonPublisher.Notify(A<AmazonPublisherMessage>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void AmazonService_SendRelevantMessage_NotifyHappenedWithRelevantData()
        {
            //Arrange

            //Act
            _amazonService.SendMessage(_command, _message);

            //Assert
            A.CallTo(() => _amazonPublisher.Notify(A<AmazonPublisherMessage>.That
                .Matches(x => x.Date == _message && x.Command == _command)))
            .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void AmazonService_TryingToSendWrongCommand_NotifyWontHappened()
        {
            //Arrange
            var command = "-1";

            //Act
            _amazonService.SendMessage(command, _message);

            //Assert
            A.CallTo(() => _amazonPublisher.Notify(A<AmazonPublisherMessage>._)).MustNotHaveHappened();
        }
    }
}
