﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Text.aspx.cs" Inherits="CapstoneWebApplication.Subject.Text1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Subject Interface </title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .blink{
        animation-name:blinker;
        animation-duration: 1s;
        animation-timing-function: linear;
        animation-iteration-count: infinite;
        -webkit-animation-name:blinker;
        -webkit-animation-duration: 2s;
        -webkit-animation-timing-function:linear;
        -webkit-animation-iteration-count:infinite;
        color:red;
        }
        @keyframes blinker{
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
        </style>
</head>
    <body>
        <form id="form1" runat="server" align="center">
        <div id="content" class="container - fluid" align ="center">
        <div class="jumbtron" align="center">
            <h2> TASK </h2>
         </div>
        <div class="panel-body">
           <div class ="form-group">
               <div class="row topmarg">
                    <div class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3"> 
                           <h3><asp:Label ID="Label_Value" runat="server" Text="LabelValue"></asp:Label></h3><br />
                            <asp:ScriptManager ID ="AudioScript" runat ="Server"></asp:ScriptManager>
                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <span class="blink">
                                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                                        </span>
                                    <br/>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
             </div>
<br />
             <form class="form-horizontal">
                   <label for="TextBox1" class ="col-sm-2 control-label" align ="center">Response</label>
                    <div class="col-sm-10">
                    <textarea class ="form-control" id ="inputResponse" rows ="3" placeholder ="Please type your response" runat="server">
                    </textarea>
                        </div>
                </form>
                <div class ="col-md-4 text-center">
                    <asp:Button ID="Save_Button" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="Save_Button_Click"/>
                     <asp:Button ID="NextButton" runat="server" Text="NEXT" CssClass ="btn btn-link" OnClick="NextButton_Click" />
               </div>
            </div>
        </form> 
    </body>
</html>
