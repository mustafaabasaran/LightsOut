using LightsOut.WindowsForm.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LightsOut.WindowsForm.UnitTests
{
    public class BoardUnitTest
    {
        private readonly Mock<IHttpClientFactory> _validMockHttpClient;
        private readonly Mock<IHttpClientFactory> _mockEmptyHttpClient;
        private readonly Mock<IHttpClientFactory> _mockWrongHttpClient;
        private readonly Mock<IHttpClientFactory> _mockWrongBoardSettingsClient;

        public BoardUnitTest()
        {
            _validMockHttpClient = MockIHttpClientFactory.GetValidMockIHttpClientFactory();
            _mockEmptyHttpClient = MockIHttpClientFactory.GetEmptyMockIHttpClientFactory();
            _mockWrongHttpClient = MockIHttpClientFactory.GetWrongdMockIHttpClientFactory();
            _mockWrongBoardSettingsClient = MockIHttpClientFactory.GetWrongColorMockIHttpClientFactory();
        }

        [Fact]
        public void LightClick_TopLeftInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            //Set all lights to be off
            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            // Click top left corner button
            testBoard.InvertHandler(testBoard.lights[0, 0], 0, 0);

            testBoard.lightsMatrix[0, 0].ShouldBe(true);
            testBoard.lightsMatrix[0, 1].ShouldBe(true);
            testBoard.lightsMatrix[1, 0].ShouldBe(true);

        }

        [Fact]
        public void CheckStatus_FindEnd()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            // Set all lights to be off
            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            bool expected = true;
            bool actual = testBoard.CheckStatus();
            actual.ShouldBe(expected);
        }

        [Fact]
        public void CheckStatus_FindNotEnd()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            testBoard.lightsMatrix[1, 1] = true;

            bool expected = false;
            bool actual = testBoard.CheckStatus();
            actual.ShouldBe(expected);
        }

        [Fact]
        public void RandomStart_OneLightOn()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            bool expected = true;
            bool actual = false;

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    // Check if light is on
                    if (testBoard.lightsMatrix[i, j] == true)
                    {
                        actual = true;
                    }
                }
            }

            actual.ShouldBe(expected);
        }

        [Fact]
        public void LightClick_NormalInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            // Click middle button
            testBoard.InvertHandler(testBoard.lights[2, 2], 2, 2);

            testBoard.lightsMatrix[2, 2].ShouldBe(true);
            testBoard.lightsMatrix[2, 3].ShouldBe(true);
            testBoard.lightsMatrix[2, 1].ShouldBe(true);
            testBoard.lightsMatrix[1, 2].ShouldBe(true);
            testBoard.lightsMatrix[3, 2].ShouldBe(true);
        }

        [Fact]
        public void LightClick_TopRightInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.InvertHandler(testBoard.lights[0, 4], 0, 4);

            testBoard.lightsMatrix[0,4].ShouldBe(true);
            testBoard.lightsMatrix[0,3].ShouldBe(true);
            testBoard.lightsMatrix[1,4].ShouldBe(true);
        }

        [Fact]
        public void LightClick_TopMiddleInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.InvertHandler(testBoard.lights[0, 2], 0, 2);

            testBoard.lightsMatrix[0,2].ShouldBe(true);
            testBoard.lightsMatrix[0,3].ShouldBe(true);
            testBoard.lightsMatrix[0,1].ShouldBe(true);
            testBoard.lightsMatrix[1,2].ShouldBe(true);
        }

        [Fact]
        public void LightClick_LeftMiddleInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.InvertHandler(testBoard.lights[2, 0], 2, 0);

            testBoard.lightsMatrix[2,0].ShouldBe(true);
            testBoard.lightsMatrix[1,0].ShouldBe(true);
            testBoard.lightsMatrix[3,0].ShouldBe(true);
            testBoard.lightsMatrix[2,1].ShouldBe(true);
        }

        [Fact]
        public void LightClick_RightMiddleInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            //Set all lights to be off
            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.InvertHandler(testBoard.lights[2, 4], 2, 4);

            testBoard.lightsMatrix[2,4].ShouldBe(true);
            testBoard.lightsMatrix[1,4].ShouldBe(true);
            testBoard.lightsMatrix[3,4].ShouldBe(true);
            testBoard.lightsMatrix[2,3].ShouldBe(true);
        }

        [Fact]
        public void LightClick_BottomMiddleInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.InvertHandler(testBoard.lights[4, 2], 4, 2);

            testBoard.lightsMatrix[4, 2].ShouldBe(true);
            testBoard.lightsMatrix[4, 1].ShouldBe(true);
            testBoard.lightsMatrix[4, 3].ShouldBe(true);
            testBoard.lightsMatrix[3, 2].ShouldBe(true);
        }

        [Fact]
        public void LightClick_BottomLeftInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            //Set all lights to be off
            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            // Click bottom left button
            testBoard.InvertHandler(testBoard.lights[4, 0], 4, 0);

            testBoard.lightsMatrix[4, 0].ShouldBe(true);
            testBoard.lightsMatrix[4, 1].ShouldBe(true);
            testBoard.lightsMatrix[3, 0].ShouldBe(true);
        }

        [Fact]
        public void LightClick_BottomRightInvert()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            // Click bottom right button
            testBoard.InvertHandler(testBoard.lights[4, 4], 4, 4);

            testBoard.lightsMatrix[4,4].ShouldBe(true);
            testBoard.lightsMatrix[4,3].ShouldBe(true);
            testBoard.lightsMatrix[3,4].ShouldBe(true);
        }

        [Fact]
        public void LightClick_LastLight()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            for (int i = 0; i < testBoard.lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < testBoard.lightsMatrix.GetLength(0); j++)
                {
                    testBoard.lightsMatrix[i, j] = false;
                }
            }

            testBoard.lightsMatrix[2, 2] = true;
            testBoard.lightsMatrix[2, 1] = true;
            testBoard.lightsMatrix[2, 3] = true;
            testBoard.lightsMatrix[1, 2] = true;
            testBoard.lightsMatrix[3, 2] = true;

            testBoard.InvertHandler(testBoard.lights[2, 2], 2, 2);

            bool expected = true;
            bool actual = testBoard.CheckStatus();
            actual.ShouldBe(expected);
        }

        [Fact]
        public void InvertButton_ChangeStatus()
        {
            var testBoard = new Board(_validMockHttpClient.Object);

            testBoard.lightsMatrix[0, 0] = false;

            testBoard.InvertTheButton(testBoard.lights[0, 0], 0, 0);

            bool expected = true;

            testBoard.lightsMatrix[0, 0].ShouldBe(expected);
        }

        [Fact]
        public void InvertButton_ChangeColor()
        {
            // Generate new game
            var testBoard = new Board(_validMockHttpClient.Object);

            // Set light to black
            testBoard.lights[0, 0].BackColor = Color.Black;

            // Invert set light
            testBoard.InvertTheButton(testBoard.lights[0, 0], 0, 0);

            bool expected = true;
            bool actual = false;

            // If light is no longer black then it worked
            if (testBoard.lights[0, 0].BackColor != Color.Black)
            {
                actual = true;
            }

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Succesfull_Http_Call_With_Empty_Response()
        {
            var testBoard = new Board(_mockEmptyHttpClient.Object);

            bool expected = true;

            testBoard.isErrorOccured.ShouldBe(expected);
        }

        [Fact]
        public void Succesfull_Http_Call_With_Wrong_Response()
        {
            var testBoard = new Board(_mockWrongHttpClient.Object);

            bool expected = true;

            testBoard.isErrorOccured.ShouldBe(expected);
        }

        [Fact]
        public void Succesfull_Http_Call_With_Wrong_BoardSetting()
        {
            var testBoard = new Board(_mockWrongBoardSettingsClient.Object);

            bool expected = true;

            testBoard.isErrorOccured.ShouldBe(expected);
        }
    }
}
