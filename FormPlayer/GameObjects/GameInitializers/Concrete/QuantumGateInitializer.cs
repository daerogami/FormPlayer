using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using QuantumGate.GameObjects.Actors.Concrete;
using QuantumGate.GameObjects.Actors.Factories;
using QuantumGate.GameObjects.Beats.Concrete;
using QuantumGate.GameObjects.Contents.Concrete;
using QuantumGate.GameObjects.Cues.Concrete;
using QuantumGate.GameObjects.GameInitializers.Abstract;
using QuantumGate.GameObjects.GameInitializers.Interface;

namespace QuantumGate.GameObjects.GameInitializers.Concrete
{
    public sealed class QuantumGateInitializer : GameDataInitializer
    {
        public override string CachePath => Path.Combine($@"{CacheFolderPath}", "QuantumGateOne");
        private string StagesPath => Path.Combine($@"{CachePath}", $@"{nameof(Stage)}s.json");
        public override IStoryData StoryData { get; protected set; }
        public string StoryDataPath => StoryData.ExternalGameDataPath;


        public QuantumGateInitializer(ProgressBar progressBar, bool forceValidation = false)
            : base(progressBar)
        {
            if (!FindGameData())
            {
                var dialog = new FolderBrowserDialog();
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ExternalGameDataPath = dialog.SelectedPath;
                }
                if (!string.IsNullOrEmpty(ExternalGameDataPath) && !File.Exists(Path.Combine(ExternalGameDataPath, "QGATE.EXE")))
                {
                    throw new FileNotFoundException("File not found", "QGATE.EXE");
                }
            }
            StoryData = new QuantumGateStoryData(ExternalGameDataPath);
            ForceValidation = forceValidation;
        }

        private bool FindGameData()
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                if (!File.Exists(Path.Combine(drive, "QGATE.EXE"))) continue;
                ExternalGameDataPath = drive;
                return true;
            }
            return false;
        }

        public override void VerifyGameData()
        {
            if (CacheIsBuilt() && !ForceValidation)
            {
                return;
            }

            var badFiles = new HashSet<string>();
            _progressBar.Visible = true;
            _progressBar.Text = @"Verifying Game Data";
            var validationFiles = File.ReadAllLines(@".\Resources\QuantumGate\manifest.dat");
            _progressBar.Maximum = validationFiles.Length;
            
            var movies = validationFiles.Where(path => path.EndsWith(".MOV"));
            ValidateMovies(movies, badFiles);
            
            var images = validationFiles.Where(path => path.EndsWith(".BMP") || path.EndsWith(".HSB"));
            ValidateImages(images, badFiles);
            
            var sounds = validationFiles.Where(path => path.EndsWith(".WAV"));
            ValidateSounds(sounds, badFiles);
            
            _progressBar.Text = @"Validation Complete";

            if (!IgnoreValidationExceptions && badFiles.Any())
            {
                throw new InvalidGameDataException(badFiles);
            }

            BuildGameCache();
        }
        
        public override void BuildGameCache()
        {
            if (!Directory.Exists(CachePath))
            {
                Directory.CreateDirectory(CachePath);
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto
            };
            var serializer = JsonSerializer.CreateDefault(settings);
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Converters.Add(new KeyValuePairConverter());
            CreateStages(serializer);
            LoadStages(serializer);
        }

        private void LoadStages(JsonSerializer serializer)
        {
            IEnumerable<Stage> stageManifest;
            using (var sr = new StreamReader(StagesPath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                stageManifest = serializer.Deserialize<List<Stage>>(reader);
            }
            foreach (var entry in stageManifest)
            {
                StoryData.AddStage(entry);
            }
        }

        [Obsolete("Only for initializing manifest")]
        private void CreateStages(JsonSerializer serializer)
        {
            using (var sw = new StreamWriter(StagesPath, false))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                var screen = new Stage("MENUSCRN");
                screen.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\SPLASH\MENUSCRN.BMP"), "MENUSCRN"));
                var screenIntroVideoActor = new VideoActor
                {
                    Content = new Video("Intro", Path.Combine(StoryDataPath, @"CONTENT\SPLASH\QGTITLE.MOV")),
                    ControlName = "Intro"
                };
                var loadCue = new OnLoadCue();
                loadCue.Beats.Add(new TriggerVideoStartBeat());
                screenIntroVideoActor.Cues.Add(loadCue);
                screen.Actors.Add(screenIntroVideoActor);
                var main = new Stage("MAINMENU");
                main.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\SPLASH\MAINMENU.BMP"), "MAINMENU"));
                var stage1 = new Stage("N3A");
                stage1.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3A.BMP"), "N3A"));
                var stage2 = new Stage("N3B");
                stage2.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3B.BMP"), "N3B"));
                var stage3 = new Stage("N3C");
                stage3.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3C.BMP"), "N3C"));
                var stage4 = new Stage("N3D");
                stage4.Scenes.Add(0, new Image(Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3D.BMP"), "N3D"));
                // Link stage transitions
                stage1.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.RightNavigation, stage2.Name, new Video("N3A_3B", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3A_3B.MOV"))));
                stage1.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.LeftNavigation, stage4.Name, new Video("N3A_3D", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3A_3D.MOV"))));
                stage2.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.RightNavigation, stage3.Name, new Video("N3B_3C", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3B_3C.MOV"))));
                stage2.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.LeftNavigation, stage1.Name, new Video("N3B_3A", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3B_3A.MOV"))));
                stage3.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.RightNavigation, stage4.Name, new Video("N3C_3D", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3C_3D.MOV"))));
                stage3.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.LeftNavigation, stage2.Name, new Video("N3C_3B", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3C_3B.MOV"))));
                stage4.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.RightNavigation, stage1.Name, new Video("N3D_3A", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3D_3A.MOV"))));
                stage4.Actors.Add(HotspotActorFactory.CreateStateChangeNavigation(NavigationHotspot.LeftNavigation, stage3.Name, new Video("N3D_3C", Path.Combine(StoryDataPath, @"CONTENT\DREWQTRS\N3D_3C.MOV"))));
                var obj = new[] { screen, main, stage1, stage2, stage3, stage4 };
                serializer.Serialize(writer, obj);
            }
        }

        private bool CacheIsBuilt()
        {
            if (!Directory.Exists(CachePath))
            {
                return false;
            }

            var files = Directory.EnumerateFiles(CachePath);
            if (!files.Any())
            {
                return false;
            }

            return false; //return true;
        }
    }
}