<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Text.aspx.cs" Inherits="CapstoneWebApplication.Subject.Text1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Subject Interface </title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="CustomStyles.css" rel="stylesheet" />
    <link href="css/bootstrap-slider.min.css" rel="stylesheet" />
    <link href="flaticon.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.1.1.js"></script>
    <script src="Scripts/bootstrap-slider.min.js"></script>
    <script src="Scripts/XMLWriter.js"></script>
    <script src="Scripts/Global.js"></script>
    <link rel="stylesheet" href="styles/styles.css" type="text/css" media="screen" charset="utf-8"/>
</head>
    <body>
        <form id="form1" runat="server" align="center">
            <div id="content" class="container" align ="center">

            <div class ="panel-body">
                <image src="https://s3.us-east-2.amazonaws.com/capstone.uhcl/univ-of-houston.jpg" runat="server" usemap="#workmap" style="width: 415px; height: 150px" /></image>
                <map name="workmap" >
                <area shape ="rect" coords ="34,44,270,350">
            </div>
            </br>
            </br>
            <div class="jumbtron" align="center">
                <div class ="panel-body">
                    <h1><b><ins>Task 1</ins> : Problem 1</b></h1>
                </div>
                <br/>
                <br/>
                <div class="panel-body">
                    <div class="row topmarg">
                        <div class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3">
                            <asp:Label ID="s3Placeholder" runat="server"></asp:Label> 
                            <h1>
                                <asp:Label ID="Label_Value" runat="server" Text="LabelValue"></asp:Label>
                            </h1>
                            </br>
                            </br>
                            <asp:ScriptManager ID ="AudioScript" runat ="Server"></asp:ScriptManager>
                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                                    <br/>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>                                  
                        <br/>
                    </div>
                </div>
            </div>
            <br/>
            <br/>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="save_Click" Text="SAVE" />
            <asp:Button ID="NextButton" runat="server" Text="NEXT" OnClick="Button2_Click" />   
        </form>
    </body>
</html>
