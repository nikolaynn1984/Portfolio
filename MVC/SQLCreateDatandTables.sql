
CREATE TABLE [dbo].[File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](125) NULL,
	[FileTypeId] [int] NULL,
	[FolderId] [int] NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Folder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Folder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FileType](
	[Id] [int] NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (1 ,'.cs'  ,'cs.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (2 ,'.xml'  ,'xml.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (3 ,'.xaml'  ,'xaml.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (4 ,'.txt'  ,'txt.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (5 ,'.svg'  ,'svg.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (6 ,'.json'  ,'json.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (7 ,'.html'  ,'default.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (8 ,'.css'  ,'default.png')
INSERT INTO [dbo].[FileType] ([Id] ,[Type] ,[Icon]) VALUES  (9 ,'default'  ,'default.png')


INSERT INTO [dbo].[Folder]  ([Name] ,[ParentId]) VALUES (N'Проект' ,0)
INSERT INTO [dbo].[Folder]  ([Name] ,[ParentId]) VALUES (N'bin' ,1)
INSERT INTO [dbo].[Folder]  ([Name] ,[ParentId]) VALUES (N'Resource' ,1)

INSERT INTO [dbo].[File]([Name]  ,[Description] ,[FileTypeId] ,[FolderId] ,[Content])
     VALUES  ('App.xaml' , 'App.xaml' ,3  ,1 ,'<Application x:Class="Modul10DZ.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:Modul10DZ"
             xmlns:UI="clr-namespace:Modul10DZ.UI"
             StartupUri="View/BotWindow.xaml">
    <Application.Resources>
        <UI:ImagePathConverter x:Key="ImagePathConverter"/>
        <!--Сообщение от пользователя-->
        <DataTemplate  x:Key="messageClien" >
            <Grid Margin="0"  Background="Transparent" HorizontalAlignment="Left" >
                <Border BorderThickness="1"  CornerRadius="0,7,7,7" Margin="5" BorderBrush="#FF79D0EE" 
                                Background="#FFB3DFEE" >
                    <Grid Margin="5"  >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <TextBlock Text="{Binding Name}"/>
                        <TextBlock Grid.Column="1" Text="{Binding CreateTime}"/>
                        <TextBlock Grid.Row="1" Grid.ColumnSpan="2" Text="{Binding MessageText}"  FontSize="18" />
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
        <!--Сообщение от админа-->
        <DataTemplate x:Key="messageAdmin" >
            <Grid   Background="Transparent" HorizontalAlignment="Right">
                <Border BorderThickness="1"  CornerRadius="7,0,7,7" Margin="5" BorderBrush="#FF4BDC9C" 
                                Background="#FF88DAB6" >
                    <Grid Margin="5" >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <TextBlock Text="{Binding StringFormat=Кому, Path=Name}" />
                        <TextBlock Grid.Column="1" Text="{Binding CreateTime}"/>
                        <TextBlock Grid.Row="1" Grid.ColumnSpan="2" Text="{Binding MessageText}"  FontSize="18"  />
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
        <!--Фото от админа-->
        <DataTemplate x:Key="imageAdmin" >
            <Grid   Background="Transparent" HorizontalAlignment="Right">
                <Border BorderThickness="2"  CornerRadius="7,0,7,7" Margin="5" BorderBrush="#FF4BDC9C" 
                                Background="#FF88DAB6" >
                    <Grid  >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <Grid>

                            <Border MaxWidth="350" Grid.ColumnSpan="2" MaxHeight="350" CornerRadius="0,7,7,7"  BorderBrush="#FF4BDC9C" BorderThickness="2"  >
                                <Image Stretch="Uniform"   
                                   Source="{Binding Path=FileName, Converter={StaticResource ImagePathConverter}}">
                                    
                                </Image>
                            </Border>

                            <Border VerticalAlignment="Bottom" HorizontalAlignment="Left" Margin="2" CornerRadius="3" Background="#E5252526">
                                <TextBlock  Grid.Column="0" Text="{Binding StringFormat=Кому, Path=Name}"  Margin="2" Foreground="White" />
                            </Border>
                            <Border Background="#E5252526" VerticalAlignment="Bottom" HorizontalAlignment="Right" Margin="2" CornerRadius="3">
                                <TextBlock Grid.Column="1" Text="{Binding CreateTime}"  Margin="2" Foreground="White"/>
                            </Border>
                        </Grid>

                        <TextBlock Grid.ColumnSpan="2" Grid.Row="1" Text="{Binding MessageCaption}"/>
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
        <!--Фото от пользователя-->
        <DataTemplate x:Key="imageClient" >

            <Grid   Background="Transparent" HorizontalAlignment="Left">
                <Border BorderThickness="2"  CornerRadius="0,7,7,7" Margin="5" BorderBrush="#FF79D0EE" 
                                Background="#FFB3DFEE" >
                    <Grid  >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <Grid>

                            <Border MaxWidth="350" Grid.ColumnSpan="2" MaxHeight="350" CornerRadius="0,7,7,7" BorderBrush="#FF79D0EE" BorderThickness="2"  >
                                <Image Stretch="Fill"   
                                   Source="{Binding Path=FileName, Converter={StaticResource ImagePathConverter}}">

                                </Image>
                            </Border>

                            <Border VerticalAlignment="Bottom" HorizontalAlignment="Left" Margin="2" CornerRadius="3" Background="#E5252526">
                                <TextBlock  Grid.Column="0" Text="{Binding StringFormat=Кому, Path=Name}"  Margin="2" Foreground="White" />
                            </Border>
                            <Border Background="#E5252526" VerticalAlignment="Bottom" HorizontalAlignment="Right" Margin="2" CornerRadius="3">
                                <TextBlock Grid.Column="1" Text="{Binding CreateTime}"  Margin="2" Foreground="White"/>
                            </Border>
                        </Grid>

                        <TextBlock Grid.ColumnSpan="2" Grid.Row="1" Text="{Binding MessageCaption}"/>
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
        <!--Аудио от админа-->
        <DataTemplate x:Key="audioAdmin" >
            <Grid   Background="Transparent" HorizontalAlignment="Right">
                <Border BorderThickness="1"  CornerRadius="7,0,7,7" Margin="5" BorderBrush="#FF4BDC9C" 
                                Background="#FF88DAB6" >
                    <Grid Margin="5" >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <StackPanel  Grid.RowSpan="2" Name="AudioPlay" Margin="3" >

                            <MediaElement UnloadedBehavior="Close"  LoadedBehavior="Manual" Name="my_media"></MediaElement>
                            <StackPanel Orientation="Horizontal">
                                <StackPanel.Triggers>
                                    <EventTrigger RoutedEvent="Button.Click" SourceName="cmd_play">
                                        <EventTrigger.Actions>
                                            <BeginStoryboard Name="MediaStoryboard">
                                                <Storyboard>
                                                    <MediaTimeline Storyboard.TargetName="my_media"
                                                       Source="{Binding FileName}"></MediaTimeline>
                                                </Storyboard>
                                            </BeginStoryboard>
                                        </EventTrigger.Actions>
                                    </EventTrigger>
                                    <EventTrigger RoutedEvent="Button.Click" SourceName="cmd_stop">
                                        <EventTrigger.Actions>
                                            <StopStoryboard BeginStoryboardName="MediaStoryboard"></StopStoryboard>
                                        </EventTrigger.Actions>
                                    </EventTrigger>
                                </StackPanel.Triggers>
                                <Button Name="cmd_play"  Background="Transparent" BorderThickness="0">
                                    <Image>
                                        <Image.Source>
                                            <BitmapImage UriSource="Images/ic_play_circle_filled_black_24dp.png"/>
                                        </Image.Source>
                                    </Image>
                                </Button>

                                <Button Name="cmd_stop" Background="Transparent" BorderThickness="0">
                                    <Image>
                                        <Image.Source>
                                            <BitmapImage UriSource="Images/ic_pause_circle_outline_black_24dp.png"/>
                                        </Image.Source>
                                    </Image>
                                </Button>
                            </StackPanel>
                        </StackPanel>
                        <TextBlock  Grid.Column="2" Grid.Row="1" Text="{Binding  Name}" />
                        <TextBlock Grid.Column="1" Grid.Row="1" Text="{Binding CreateTime}"/>
                        <TextBlock Grid.ColumnSpan="3" Grid.Row="2" Text="{Binding MessageCaption}"/>
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
        <!--Аудио от пользователя-->
        <DataTemplate x:Key="audioClient" >
            <Grid   Background="Transparent" HorizontalAlignment="Left">
                <Border BorderThickness="1"  CornerRadius="0,7,7,7" Margin="5" BorderBrush="#FF79D0EE" 
                                Background="#FFB3DFEE" >
                    <Grid Margin="5" >
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <StackPanel  Grid.RowSpan="2" Name="AudioPlay" Margin="3" >

                            <MediaElement UnloadedBehavior="Close"  LoadedBehavior="Manual" Name="my_media"></MediaElement>
                            <StackPanel Orientation="Horizontal">
                                <StackPanel.Triggers>
                                    <EventTrigger RoutedEvent="Button.Click" SourceName="cmd_play">
                                        <EventTrigger.Actions>
                                            <BeginStoryboard Name="MediaStoryboard">
                                                <Storyboard>
                                                    <MediaTimeline Storyboard.TargetName="my_media"
                                                       Source="{Binding FileName}"></MediaTimeline>
                                                </Storyboard>
                                            </BeginStoryboard>
                                        </EventTrigger.Actions>
                                    </EventTrigger>
                                    <EventTrigger RoutedEvent="Button.Click" SourceName="cmd_stop">
                                        <EventTrigger.Actions>
                                            <StopStoryboard BeginStoryboardName="MediaStoryboard"></StopStoryboard>
                                        </EventTrigger.Actions>
                                    </EventTrigger>
                                </StackPanel.Triggers>
                                <Button Name="cmd_play"  Background="Transparent" BorderThickness="0">
                                    <Image>
                                        <Image.Source>
                                            <BitmapImage UriSource="Images/ic_play_circle_filled_black_24dp.png"/>
                                        </Image.Source>
                                    </Image>
                                </Button>

                                <Button Name="cmd_stop" Background="Transparent" BorderThickness="0">
                                    <Image>
                                        <Image.Source>
                                            <BitmapImage UriSource="Images/ic_pause_circle_outline_black_24dp.png"/>
                                        </Image.Source>
                                    </Image>
                                </Button>
                            </StackPanel>
                        </StackPanel>
                        <TextBlock  Grid.Column="2" Grid.Row="1" Text="{Binding  Name}" />
                        <TextBlock Grid.Column="1" Grid.Row="1" Text="{Binding CreateTime}"/>
                        <TextBlock Grid.ColumnSpan="3" Grid.Row="2" Text="{Binding MessageCaption}"/>
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
    </Application.Resources>
</Application>
')
INSERT INTO [dbo].[File]([Name]  ,[Description] ,[FileTypeId] ,[FolderId] ,[Content])
     VALUES  ('BotWindow.xaml' , 'BotWindow.xaml' ,3  ,1 ,'<Window x:Class="Modul10DZ.View.BotWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Modul10DZ.View" xmlns:UI="clr-namespace:Modul10DZ.UI"
        mc:Ignorable="d"
        Title="BotWindow" MinHeight="350" Height="450" MinWidth="700" Width="800" Background="Beige">
    <Window.Resources>
        <UI:ImagePathConverter x:Key="ImagePathConverter"/>

    </Window.Resources>
    <Grid >
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="250"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="Auto"/>
            </Grid.RowDefinitions>
            <ListView Name="listUsers" 
                  HorizontalContentAlignment="Stretch"
                  BorderBrush="Transparent"
                  Background="Transparent"
                  SelectionChanged="listUsers_SelectionChanged"
                  >
                <ListView.ItemContainerStyle>
                    <Style>
                        <Setter Property="Control.Padding" Value="0"/>

                        <Style.Triggers>
                            <Trigger Property="ListViewItem.IsSelected" Value="True">
                                <Setter Property="ListViewItem.Background" Value="White"/>
                                <Setter Property="ListViewItem.BorderThickness" Value="1"/>
                                <Setter Property="ListViewItem.BorderBrush" Value="White"/>
                            </Trigger>
                        </Style.Triggers>
                    </Style>
                </ListView.ItemContainerStyle>
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <Grid Margin="0" Background="Transparent">
                            <Border  BorderThickness="1" BorderBrush="White" Padding="3"
                                Background="{Binding Path=Background, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type ListViewItem}}}">
                                <StackPanel >
                                    <TextBlock Text="{Binding Nick}"  FontSize="18"/>
                                    <TextBlock Text="{Binding StringFormat=Дата добавления {0:dd/MM/yyyy},Path=DateAdded }" Foreground="Gray"/>
                                </StackPanel>
                            </Border>
                        </Grid>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
            <TextBlock x:Name="serverWarning" Grid.Row="1" Foreground="Red" FontWeight="Bold" Margin="5"/>
        </Grid>
       

        <Grid Grid.Column="1">
            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="40"/>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="Auto"/>
            </Grid.ColumnDefinitions>
            <Grid  Grid.ColumnSpan="2"  >
                <Grid.RowDefinitions>
                    <RowDefinition Height="*"/>
                    <RowDefinition Height="Auto"/>
                </Grid.RowDefinitions>
                <Grid DockPanel.Dock="Top">
                    <ListView Name="listMessage" Margin="5" 
                      HorizontalContentAlignment="Stretch"
                       
                      Background="White"
                      BorderThickness="0" 
                      >
                        <ListView.ItemTemplateSelector>
                            <UI:MessageDataTemplateSelector
                        messageClien="{StaticResource messageClien}"
                        messageAdmin="{StaticResource messageAdmin}"
                        imageAdmin="{StaticResource imageAdmin}"
                        imageClient="{StaticResource imageClient}"
                        audioAdmin="{StaticResource audioAdmin}"
                        audioClient="{StaticResource audioClient}"/>
                        </ListView.ItemTemplateSelector>
                    </ListView>
                </Grid>
               
            </Grid>
           
            
            <TextBox x:Name="txtMessage" Grid.Row="1" Margin="5" FontSize="14" MouseEnter="txtMessage_MouseEnter" Text="Написать сообщение" Foreground="Gray" MouseLeave="txtMessage_MouseLeave" BorderBrush="Transparent"/>
            <Button x:Name="btnSend"  Grid.Row="1" Grid.Column="1" Content="Отправить" Margin="5" IsEnabled="False" Click="btnSend_Click" BorderBrush="White" Padding="5"/>
           
        </Grid>
    </Grid>
    
</Window>
')
INSERT INTO [dbo].[File]([Name]  ,[Description] ,[FileTypeId] ,[FolderId] ,[Content])
     VALUES  ('BotWindow.xaml.cs' , 'BotWindow.xaml.cs' ,1  ,1 ,'using Modul10DZ.Client;
using Modul10DZ.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telegram.Bot.Types;

namespace Modul10DZ.View
{

    /// <summary>
    /// Логика взаимодействия для BotWindow.xaml
    /// </summary>
    public partial class BotWindow : Window
    {
        
        static TelegramMessageClient client;
        static long UserId;
        private string UserNick;


        public BotWindow()
        {
            InitializeComponent();
            client = new TelegramMessageClient(this);
            listUsers.ItemsSource = client.Users;
            listMessage.ItemsSource = client.Messages;
            client.Messages.CollectionChanged += Messages_CollectionChanged;

        }

        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            LoadMessages();
        }

        private  void LoadMessages()
        {
 
                var list = client.Messages.Where(s => s.UserId == UserId).OrderBy(o => o.CreateDate + o.CreateTime).ToList();
                var observableList = new ObservableCollection<Messages>(list);
                listMessage.ItemsSource = observableList; 


            
        }
        private void listUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageLog mes = (MessageLog)listUsers.SelectedItem;
            UserId  = mes.UserId;
            UserNick = mes.Nick;
            btnSend.IsEnabled = true;
            LoadMessages();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            client.SendMessage(txtMessage.Text, UserId, UserNick);

            txtMessage.Text = String.Empty;
            
        }

        private void txtMessage_MouseEnter(object sender, MouseEventArgs e)
        {
            if(txtMessage.Text == "Написать сообщение")
            {
                txtMessage.Text = null;
                txtMessage.Foreground = Brushes.Black;
            }
        }

        private void txtMessage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(txtMessage.Text))
            {
                txtMessage.Text = "Написать сообщение";
                txtMessage.Foreground = Brushes.Gray;
            }
        }

      

       
    }
}')