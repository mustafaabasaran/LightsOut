using LightsOut.WindowsForm.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut.WindowsForm
{
    public partial class Board : Form
    {
        public Button[,] lights;
        public bool[,] lightsMatrix;
        public bool isErrorOccured;

        private readonly IHttpClientFactory _clientFactory;
        private BoardSetting _boardSetting;
        private List<InitialState> _initialStates;
        private System.Drawing.Color offColor;
        private System.Drawing.Color onColor;

        public Board(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            CreateForm();
        }

        public MessageBoxButtons MB_OK { get; private set; }

        public async void CreateForm()
        {
            await GetConfigurations();
            ValidateConfigurations();

            if (!isErrorOccured)
            {
                InitializeComponent();
                CreateBoard();
            }
        }

        private async Task GetConfigurations()
        {
            var client = _clientFactory.CreateClient("api");

            try
            {
                var settingsRequest = new HttpRequestMessage(HttpMethod.Get, Resources.SettingsRoute);
                var settingsResponse = await client.SendAsync(settingsRequest);
                if (settingsResponse.IsSuccessStatusCode)
                {
                    using var responseStream = await settingsResponse.Content.ReadAsStreamAsync();

                    var settingResponseObject = await System.Text.Json.JsonSerializer.DeserializeAsync
                            <ServiceResponseModel<BoardSetting>>(responseStream);
                    if (settingResponseObject.Header.StatusCode == (int)HttpStatusCode.OK)
                    {
                        _boardSetting = settingResponseObject.Data;
                    }
                }
                else
                {
                    ShowErrorBox(Resources.ErrorWhenGettingBoardSetting);
                }
            }
            catch (Exception ex)
            {
                ShowErrorBox($"Error when getting the board settings. Error {ex.Message}");
            }

            try
            {
                var initialStateRequest = new HttpRequestMessage(HttpMethod.Get, Resources.InitialStateRoute);
                var initialStateResponse = await client.SendAsync(initialStateRequest);
                if (initialStateResponse.IsSuccessStatusCode)
                {
                    var responseBody = await initialStateResponse.Content.ReadAsStringAsync();
                    var initialStateResponseObject = JsonConvert.DeserializeObject<ServiceResponseModel<List<InitialState>>>(responseBody);

                    if (initialStateResponseObject.Header.StatusCode == (int)HttpStatusCode.OK)
                    {
                        _initialStates = initialStateResponseObject.Data;
                    }
                    else
                    {
                        ShowErrorBox(Resources.ErrorWhenGettingInitialStateList);
                    }
                }

            }
            catch (Exception ex)
            {
                ShowErrorBox($"Error when getting the initial states of the board. Error : {ex.Message}");
            }


        }

        private void ValidateConfigurations()
        {
            if (_boardSetting == null)
            {
                ShowErrorBox(Resources.BoardSettingIsNull);
                return;
            }

            if (_initialStates == null || _initialStates.Count(x=> x.State == (int)State.IsOn) == 0)
            {
                ShowErrorBox(Resources.InitialStateListIsNull);
                return;
            }

            if (_initialStates.Any(x => x.Row > _boardSetting.Size - 1)
                || _initialStates.Any(x => x.Column > _boardSetting.Size - 1))
            {
                ShowErrorBox(Resources.WrongInitialState);
                return;
            }

            if (!_boardSetting.OnColor.StartsWith("#") || !_boardSetting.OffColor.StartsWith("#"))
            {
                ShowErrorBox(Resources.WrongInitialStateColor);
                return;
            }
        }

        public void CreateBoard()
        {
            lights = new Button[_boardSetting.Size, _boardSetting.Size];
            lightsMatrix = new bool[_boardSetting.Size, _boardSetting.Size];
            offColor = System.Drawing.ColorTranslator.FromHtml(_boardSetting.OffColor);
            onColor = System.Drawing.ColorTranslator.FromHtml(_boardSetting.OnColor);

            for (int i = 0; i < lights.GetLength(1); i++)
            {
                for (int j = 0; j < lights.GetLength(0); j++)
                {
                    lights[i, j] = new Button();
                    lights[i, j].Size = new Size(40, 40);
                    lights[i, j].Name = i.ToString() + j.ToString();
                    lights[i, j].Click += light_Click;
                    lights[i, j].Location = new Point(30 + (j * 60), 20 + (i * 60));
                    lights[i, j].BackColor = offColor;
                    lightsMatrix[i, j] = false;
                    this.Controls.Add(lights[i, j]);
                }
            }

            Random rnd = new Random();

            foreach (var item in _initialStates)
            {
                if (item.State == (int)State.IsOn)
                {
                    InvertTheButton(lights[item.Row, item.Column], item.Row, item.Column);
                }
            }

            if (CheckStatus() == true)
            {
                int x = rnd.Next(0, lights.GetLength(1));
                int y = rnd.Next(0, lights.GetLength(0));
                InvertTheButton(lights[x, y], x, y);
            }
        }

        public void light_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            int i = (int)Char.GetNumericValue(b.Name[0]);
            int j = (int)Char.GetNumericValue(b.Name[1]);

            InvertHandler(lights[i, j], i, j);

            CheckIfEnded();
        }

        public void InvertHandler(object sender, int i, int j)
        {
            InvertTheButton(lights[i, j], i, j);

            if (i > 0)
            {
                InvertTheButton(lights[i - 1, j], i - 1, j); //Above
            }
            if (i < (lights.GetLength(1) - 1))
            {
                InvertTheButton(lights[i + 1, j], i + 1, j); //Below
            }
            if (j > 0)
            {
                InvertTheButton(lights[i, j - 1], i, j - 1); //Left
            }
            if (j < (lights.GetLength(1) - 1))
            {
                InvertTheButton(lights[i, j + 1], i, j + 1); //Right
            }

        }

        public void InvertTheButton(object sender, int i, int j)
        {
            Button? b = sender as Button;

            lightsMatrix[i, j] = !lightsMatrix[i, j];

            if (lightsMatrix[i, j] == true)
            {
                b.BackColor = onColor;
            }
            else
            {
                b.BackColor = offColor;
            }

        }

        public bool CheckStatus()
        {
            for (int i = 0; i < lightsMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < lightsMatrix.GetLength(0); j++)
                {
                    if (lightsMatrix[i, j] == true)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void CheckIfEnded()
        {
            // Check if all lights are off
            if (CheckStatus() == true)
            {
                MessageBox.Show("Game Completed!",
                    "Congratulations!",
                     MB_OK);
                Application.Exit();
            }
        }

        public void ShowErrorBox(string errorMessage)
        {
            Task.Run(() =>
            {
                MessageBox.Show(errorMessage, "Eror", MB_OK);
            });
            Thread.Sleep(2000);
            isErrorOccured = true;
            Application.Exit();
        }

    }
}
