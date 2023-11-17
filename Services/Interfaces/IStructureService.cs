using NormManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NormManager.Services.Interfaces
{
    public interface IStructureService
    {
        /// <summary>
        /// Название XML файла
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Структура XML
        /// </summary>
        public Main Structure { get; protected set; }

        /// <summary>
        /// Установить сущестующую структуру
        /// </summary>
        /// <param name="mainStructure">структура XML</param>
        public void SetReadyStructure(Main mainStructure);

        /// <summary>
        /// Изменить название XML файла
        /// </summary>
        /// <param name="fileName"></param>
        public void SetFileName(string fileName);

        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void AddNewFolder(string folderName);

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void RemoveFolder(string folderName);

        /// <summary>
        /// Удаление измеряемой величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название измеряемой величины</param>
        public void RemoveMeasurableQuantity(string folderName, string measurableQuantityName);

        /// <summary>
        /// Перемещение папки вверх
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void MoveUpFolder(string folderName);

        /// <summary>
        /// Перемещение папки вниз
        /// </summary>
        /// <param name="folderName">Название папки</param>
        public void MoveDownFolder(string folderName);

        /// <summary>
        /// Перемещение элемента вверх
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название измеряемой величины</param>
        public void MoveUpMeasurableQuantity(string folderName, string measurableQuantityName);

        /// <summary>
        /// Перемещение элемента вниз
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название измеряемой величины</param>
        public void MoveDownMeasurableQuantity(string folderName, string measurableQuantityName);

        /// <summary>
        /// Добавление дефолтных параметров
        /// </summary>
        public void AddDefaultParams();

        /// <summary>
        /// Добавление измеряемой величины
        /// </summary>
        /// <param name="idMeasurableQuantity">ID величины</param>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="measurableQuantityType">Тип величины</param>
        /// <param name="countParamsFromInputValue">Количество входных параметров</param>
        /// <param name="parametersIncludeInValue">Список параметров которые содержит величина</param>
        public void AddMeasureValue(string idMeasurableQuantity, string folderName, string measurableQuantityName, 
            string measurableQuantityType, int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue);

        /// <summary>
        /// Редактирование измеряемой величины
        /// </summary>
        /// <param name="idMeasurableQuantity">ID величины</param>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="measurableQuantityType">Тип величины</param>
        /// <param name="countParamsFromInputValue">Количество входных параметров</param>
        /// <param name="parametersIncludeInValue">Список параметров которые содержит величина</param>
        public void EditMeasureValue(string idMeasurableQuantity, string folderName, string measurableQuantityName, string measurableQuantityType,
            int countParamsFromInputValue, ObservableCollection<ItemOfParams> parametersIncludeInValue);

        /// <summary>
        /// Изменить название величины
        /// </summary>
        /// <param name="folderName">Название папки</param>
        /// <param name="measurableQuantityName">Название величины</param>
        /// <param name="newMeasurableQuantityName">Новое название величины</param>
        public void RenameMeasureValue(string folderName, string measurableQuantityName, string newMeasurableQuantityName);

        /// <summary>
        /// Добавление целочисленного параметра
        /// </summary>
        /// <param name="paramId">ID параметра</param>
        /// <param name="paramName">Название параметра</param>
        /// <param name="lowerbound">Нижняя граница</param>
        /// <param name="upperbound">Вверхняя граница</param>
        /// <param name="unit">Единиица измерения</param>
        /// <param name="fname">Обозначение в формулах</param>
        public void AddRealParam(string paramId, string paramName, string lowerbound, string upperbound, string unit, string fname);

        /// <summary>
        /// редактирование целочисленного параметра
        /// </summary>
        /// <param name="paramId">ID параметра</param>
        /// <param name="paramName">Название параметра</param>
        /// <param name="lowerbound">Нижняя граница</param>
        /// <param name="upperbound">Вверхняя граница</param>
        /// <param name="unit">Единиица измерения</param>
        /// <param name="fname">Обозначение в формулах</param>
        public void EditRealParam(string paramId, string paramName, string lowerbound, string upperbound, string unit, string fname);

        /// <summary>
        /// Добавление параметра c перечислением
        /// </summary>
        /// <param name="paramId">ID параметра</param>
        /// <param name="paramName">Название параметра</param>
        /// <param name="parameterOptions">Список вариантов параметра</param>
        public void AddEnumParam(string paramId, string paramName, ObservableCollection<string> parameterOptions);

        /// <summary>
        /// Редактирование параметра c перечислением
        /// </summary>
        /// <param name="paramId">ID параметра</param>
        /// <param name="paramName">Название параметра</param>
        /// <param name="parameterOptions">Список вариантов параметра</param>
        public void EditEnumParam(string paramId, string paramName, ObservableCollection<string> parameterOptions);

        /// <summary>
        /// Получить параметр
        /// </summary>
        /// <param name="paramName">Имя параметра</param>
        /// <returns></returns>
        public ItemOfParams GetParam(string paramName);

        /// <summary>
        /// Получить список объектов для разделения
        /// </summary>
        /// <param name="treeLevel">Структура</param>
        /// <param name="childrens">Результат</param>
        /// <param name="upperChildrens">Не используемый параметр для хранения дублей</param>
        /// <returns></returns>
        public List<ItemOfChildren> GetAllChildren(TreeLevel treeLevel, List<ItemOfChildren> childrens = null, List<ItemOfChildren> upperChildrens = null);

        /// <summary>
        /// Очитка структуры
        /// </summary>
        public void ClearStructure();
    }
}
