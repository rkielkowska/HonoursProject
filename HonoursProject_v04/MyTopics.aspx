<%@ Page Title="" Async="true"  Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyTopics.aspx.cs" Inherits="HonoursProject_v04.MyTopics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form runat="server" style="width:100%">
        <asp:ScriptManager runat="server" EnablePageMethods="true"  ></asp:ScriptManager>

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" Font-Bold="true" Font-Size="22px" Text="My Topics"></asp:Label><br /><br />
            </div>
        </div>

        
        <table style="width:100%">
            <tr>
               <td style="vertical-align:top">
                    <div class="row">
                        <div id="divViewTopic" class="col-md-4" runat="server" style="width: 95%">

                            <div>
                                <div>
                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true" Text="Title: "></asp:Label>
                                        <asp:Label runat="server" Font-Size="Larger" ID="lblTitle"></asp:Label>
                                    </div>

                                     <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Supervisor: "></asp:Label>
                                        <asp:Label runat="server" ID="lblSupervisor"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Subject Areas: "></asp:Label>
                                        <asp:Label runat="server" ID="lblSubjectAreas"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Suitable For: "></asp:Label>
                                        <asp:Label runat="server" ID="lblSuitableFor"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Goals Of Project: "></asp:Label>
                                        <asp:Label runat="server" ID="lblGoalsOfProject" ></asp:Label>
                                    </div>

                                     <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Project Description: "></asp:Label>
                                        <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Project Type: "></asp:Label>
                                        <asp:Label runat="server" ID="lblType"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Resources Required: "></asp:Label>
                                        <asp:Label runat="server" ID="lblResourcesRequired"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Background Needed: "></asp:Label>
                                        <asp:Label runat="server" ID="lblBackgroundNeeded"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" Font-Bold="true" Text="Recommended Reading: "></asp:Label>
                                        <asp:Label runat="server" ID="lblRecommendedReading"></asp:Label>
                                    </div>

                                    <asp:Button runat="server" Text="Cancel" ID="btnCancelView" OnClick="btnCancelView_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                </td>
            </tr>
        </table>
        
       
        

        <div class="row">
            <div class="col-md-4" style="width:100%">
                <asp:GridView ID="gvwMyTopics" runat="server"  AllowPaging="true"  AutoGenerateColumns="false" Width="100%" PageSize="15" 
                    CssClass="table table-bordered table-hover table-responsive table-condensed" OnPageIndexChanging="gridView_PageIndexChanging"
                     OnRowDeleting="gvwMyTopics_RowDeleting" DataKeyNames="_id" OnRowDataBound="gvwMyTopics_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="SubjectAreas" HeaderText="Subject Areas" />
                        <asp:BoundField DataField="SuitableFor" HeaderText="Suitable For" />
                        <asp:BoundField DataField="Type" HeaderText="Project Type" />
                        <asp:TemplateField HeaderText="Prioritise">
                            <ItemTemplate> 
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnUp" ImageUrl="~/img/up.png" runat="server"  CommandArgument='<%#Eval("_id") %>'
                                    CommandName="Up" OnCommand="Prioritise_Command" Height="30px" Width="30px"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnDown" ImageUrl="~/img/down.png" runat="server"  CommandArgument='<%#Eval("_id") %>'
                                    CommandName="Down" OnCommand="Prioritise_Command" Height="30px" Width="30px"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </table>
                                                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server"  CommandArgument='<%#Eval("_id") %>'
                                    CommandName="View" OnCommand="lnkView_Command">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("_id") %>'
                                    CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="_id" Visible="false" />
                    </Columns>
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"  FirstPageText="First" LastPageText="Last"/>
                </asp:GridView> 
            </div>
        </div>
    </form>
    
</asp:Content>
