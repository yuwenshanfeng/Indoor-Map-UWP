﻿using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using IndoorMap.Models;
using IndoorMap.Helpers;
using IndoorMap.Controller;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace IndoorMap.ViewModels
{

    [DataContract]
    public class SubMallListPage_Model : ViewModelBase<SubMallListPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        bool isLoaded = false;


        public SubMallListPage_Model()
        {
            if (!isLoaded)
            {
                SuscribeCommand();
                isLoaded = true;
            }
        }

        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<BindableBase, String> _TitleDefaultValueFactory = m => m.GetType().Name;
        #endregion

        public int SelectedIndex
        {
            get { return _SelectedIndexLocator(this).Value; }
            set { _SelectedIndexLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int SelectedIndex Setup
        protected Property<int> _SelectedIndex = new Property<int> { LocatorFunc = _SelectedIndexLocator };
        static Func<BindableBase, ValueContainer<int>> _SelectedIndexLocator = RegisterContainerLocator<int>("SelectedIndex", model => model.Initialize("SelectedIndex", ref model._SelectedIndex, ref _SelectedIndexLocator, _SelectedIndexDefaultValueFactory));
        static Func<BindableBase, int> _SelectedIndexDefaultValueFactory = m => -1;
        #endregion
        

        //MallList
        public List<MallModel> MallList
        {
            get { return _MallListLocator(this).Value; }
            set { _MallListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property Channel MallList Setup        
        protected Property<List<MallModel>> _MallList = new Property<List<MallModel>> { LocatorFunc = _MallListLocator };
        static Func<BindableBase, ValueContainer<List<MallModel>>> _MallListLocator = RegisterContainerLocator<List<MallModel>>("MallList", model => model.Initialize("MallList", ref model._MallList, ref _MallListLocator, _MallListDefaultValueFactory));
        static Func<List<MallModel>> _MallListDefaultValueFactory = () => { return new List<MallModel>(); };
        #endregion

        //CommandShowOneMallInMap
        public CommandModel<ReactiveCommand, String> CommandShowOneMallInMap
        {
            get { return _CommandShowOneMallInMapLocator(this).Value; }
            set { _CommandShowOneMallInMapLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandShowOneMallInMap Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandShowOneMallInMap = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandShowOneMallInMapLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandShowOneMallInMapLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandShowOneMallInMap", model => model.Initialize("CommandShowOneMallInMap", ref model._CommandShowOneMallInMap, ref _CommandShowOneMallInMapLocator, _CommandShowOneMallInMapDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandShowOneMallInMapDefaultValueFactory =
            model =>
            {
                var resource = "ShowAllMallInMap";           // Command resource  
                var commandId = "ShowAllMallInMap";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            MallModel mall = e.EventArgs.Parameter as MallModel;
                            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, mall, typeof(MallModel), "ListButtonClickByEventRouter", true);

                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion

        //CommandLvMallItemClick
        public CommandModel<ReactiveCommand, String> CommandLvMallItemClick
        {
            get { return _CommandLvMallItemClickLocator(this).Value; }
            set { _CommandLvMallItemClickLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandLvMallItemClick Setup        
        protected Property<CommandModel<ReactiveCommand, String>> _CommandLvMallItemClick = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandLvMallItemClickLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandLvMallItemClickLocator = RegisterContainerLocator<CommandModel<ReactiveCommand, String>>("CommandLvMallItemClick", model => model.Initialize("CommandLvMallItemClick", ref model._CommandLvMallItemClick, ref _CommandLvMallItemClickLocator, _CommandLvMallItemClickDefaultValueFactory));
        static Func<BindableBase, CommandModel<ReactiveCommand, String>> _CommandLvMallItemClickDefaultValueFactory =
            model =>
            {
                var resource = "LvMallItemClick";           // Command resource  
                var commandId = "LvMallItemClick";
                var vm = CastToCurrentType(model);
                var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model }; //New Command Core
                cmd.DoExecuteUIBusyTask(
                        vm,
                        async e =>
                        {
                            //await vm.StageManager.DefaultStage.Show(new DetailPage_Model());
                            //Todo: Add NavigateToAbout logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                            ItemClickEventArgs eventArgs = e.EventArgs.Parameter as ItemClickEventArgs;
                            var mall = eventArgs.ClickedItem as MallModel;
                            
                            MVVMSidekick.EventRouting.EventRouter.Instance.RaiseEvent(vm, mall, typeof(MallModel), "NavigateToDetailByEventRouter", true);

                            //await vm.StageManager.DefaultStage.Show(new AtlasPage_Model(mall.buildings.FirstOrDefault()));

                            //await new MessageDialog(mall.lat + " " + mall.lon).ShowAsync();
                        }
                    )

                    .DoNotifyDefaultEventRouter(vm, commandId)
                    .Subscribe()
                    .DisposeWith(vm);

                var cmdmdl = cmd.CreateCommandModel(resource);
                cmdmdl.ListenToIsUIBusy(model: vm, canExecuteWhenBusy: false);
                return cmdmdl;
            };
        #endregion


        private void GetSupportMallListAction()
        {
            string url = string.Format(@"http://op.juhe.cn/atlasyun/mall/list?key={0}&cityid={1}", Configmanager.INDOORMAP_APPKEY, AppSettings.Intance.SelectedCityId);
            FormAction action = new FormAction(url);
            action.isShowWaitingPanel = true;
            action.viewModel = this;
            action.Run();
            action.FormActionCompleted += (result, ee) =>
            {
                JsonMallModel jsonMall = JsonConvert.DeserializeObject<JsonMallModel>(result);
                if (jsonMall.reason == "成功" || jsonMall.reason == "successed")
                {
                    HttpClientReturnMallList(jsonMall.result);
                }
            };
        }
        public void HttpClientReturnMallList(List<MallModel> jsonMall)
        {
            this.MallList = jsonMall;
        }

        private void SuscribeCommand()
        {
            //MallList Item Tapped
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "CitySelectedChangedEvent")
                .Subscribe(
                e =>
                {
                    //var city = e.EventData as CityModel;
                    ////获取保存的城市
                    //AppSettings.Intance.SelectedCityId = city.id;

                    //GetSupportMallListAction();

                    var mallList = e.EventData as List<MallModel>;
                    this.MallList = mallList;

                }
                ).DisposeWith(this);

            //When Search One Mall, Mark It And go for it;
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<object>()
                .Where(x => x.EventName == "MarkSearchedMall")
                .Subscribe(
                e =>
                {
                    var mall = e.EventData as MallModel;
                    SelectedIndex = MallList.IndexOf(mall);
                }
                ).DisposeWith(this);
        }
        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{
        //    return base.OnBindedToView(view, oldValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        //{ 
        //    return base.OnBindedViewLoad(view);
        //}

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion
    }

}

