﻿using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VidCoder.Model;
using VidCoder.Services;

namespace VidCoder.ViewModel
{
	public class LogWindowViewModel : ViewModelBase
	{
		private MainViewModel mainViewModel = Ioc.Get<MainViewModel>();
		private ILogger logger = Ioc.Get<ILogger>();

		private ICommand clearLogCommand;
		private ICommand copyCommand;

		public MainViewModel MainViewModel
		{
			get
			{
				return this.mainViewModel;
			}
		}

		public ICommand ClearLogCommand
		{
			get
			{
				if (this.clearLogCommand == null)
				{
					this.clearLogCommand = new RelayCommand(() =>
					{
						this.logger.ClearLog();
					});
				}

				return this.clearLogCommand;
			}
		}

		public ICommand CopyCommand
		{
			get
			{
				if (this.copyCommand == null)
				{
					this.copyCommand = new RelayCommand(() =>
					{
						lock (this.logger.LogLock)
						{
							var logTextBuilder = new StringBuilder();

							foreach (LogEntry entry in this.logger.LogEntries)
							{
								logTextBuilder.AppendLine(entry.Text);
							}

							Ioc.Get<ClipboardService>().SetText(logTextBuilder.ToString());
						}
					});
				}

				return this.copyCommand;
			}
		}
	}
}