using ConsoleManager;

using ProgramManager;

namespace TestOldPhonePad
{
    public class UnitTest1
    {
        private ConsoleManagerStub _cmSt = null;
        private ProgramManagerTest _pmSt = null;
        private readonly ProgramManager.IProgramManager _pm;
        [Fact]
        public void OldPhonePadAndKeyDecoderClassTest()
        {
            _cmSt = new ConsoleManagerStub();
            _pmSt = new ProgramManagerTest(_cmSt);
  
            var testInputList = new List<string>
            {
                "4433555 555666#",
                "8 88777444666*664#",
                "222 2 22#",
                "444 777 666 6607777 6663338#"
            };
            var expectedOutput = new List<string>
            {
                "hello",
                "turing",
                "cab",
                "iron soft"
            };
            for(int i=0;i< testInputList.Count;i++ )
            {
                Assert.Equal(
                     _pmSt.OldPhonePad(testInputList[i]), expectedOutput[i] );

            }
            

           

        }
    }
}