using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;  
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace UI.UIElements
{
    public class UiElementManager
    {
        public List<UiElement> UiElements { get; private set; }
        private readonly ILogger<UiElementManager> _logger;

        public UiElementManager(string configFilePath, ILogger<UiElementManager> logger)
        {
            _logger = logger;

            if (!File.Exists(configFilePath))
            {
                _logger.LogError($"Конфигурационный файл {configFilePath} не найден.");
                throw new FileNotFoundException($"Конфигурационный файл {configFilePath} не найден.");
            }

            try
            {
                var json = File.ReadAllText(configFilePath);
                var config = JsonConvert.DeserializeObject<UiElementConfig>(json);
                UiElements = config?.UiElements ?? new List<UiElement>();

                if (UiElements.Count == 0)
                {
                    _logger.LogWarning("Конфигурационный файл пуст или не содержит элементов UI.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при чтении файла конфигурации: {ex.Message}");
                throw;
            }
        }

        public UiElement GetElementByName(string name)
        {
            return UiElements.FirstOrDefault(e => e.Name == name);
        }
    }
}
