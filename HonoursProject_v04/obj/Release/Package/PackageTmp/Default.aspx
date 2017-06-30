<%@ Page Title="" Async="true"  Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HonoursProject_v04.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form runat="server" style="width:100%">
        <asp:ScriptManager runat="server" EnablePageMethods="true"  ></asp:ScriptManager>

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" Font-Bold="true" Font-Size="22px" Text="Project Topics"></asp:Label><br /><br />
            </div>
        </div>

        
        <table>
            <tr>
                <td style="vertical-align:top">
                    <div class="row" id="divBtnAddNewTopic" >
                        <div class="col-md-4">
                            <table>
                                <tr>
                                    <td style="padding-right:15px">
                                        <asp:Button runat="server" Text="Add New Topic" OnClick="btnAddNewTopic_Click" ID="btnAddNewTopic"  CssClass="btn btn-primary" /> <br />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" Text="Submit Self Proposed Topic" OnClick="btnSubmitSelfProposedTopic_Click" ID="btnSubmitSelfProposedTopic"  CssClass="btn btn-primary" /> <br />
                                    </td>
                                </tr>
                            </table>
                             
                        </div>
                    </div>

                    <div class="row">
                        <div id="divAddNewTopic" class="col-md-4" runat="server" style="width: 500px; ">

                            <div>
                                <div class="form-group">
                                    <asp:Label Font-Size="16px" Font-Bold="true" ID="lblHeader" runat="server" Text="Add New Topic"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label runat="server" visible="false" id="lblError" style="color: red; font: bold"></label>
                                </div>

                                <div>
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtTitle" runat="server" Text="Title"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtSupervisor" runat="server" Text="Supervisor"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtSupervisor" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtSubjectAreas" runat="server" Text="Subject Areas"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtSubjectAreas" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtSuitableFor" runat="server" Text="Suitable For"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtSuitableFor" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtGoalsOfProject" runat="server" Text="Goals of Project"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtGoalsOfProject" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtDescription" runat="server" Text="Project Description"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtType" runat="server" Text="Type"></asp:Label>
                                        <asp:DropDownList runat="server" ID="txtType" CssClass="form-control">
                                            <asp:ListItem Text="Develop and Test" Value="Develop and Test"></asp:ListItem>
                                            <asp:ListItem Text="Experimental" Value="Experimental"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtResourcesRequired" runat="server" Text="Resources Required"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtResourcesRequired" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtBackgroundNeeded" runat="server" Text="Background Needed"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtBackgroundNeeded" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="txtRecommendedReading" runat="server" Text="Recommended Reading"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtRecommendedReading" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>

                                    <asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" />
                                    <asp:Button runat="server" Text="Submit" ID="btnAddNew" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                </td>
               <td style="padding-left: 150px; vertical-align:top">
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
            <div class="col-md-4" style="width: 1100px;">
                <table>
                    <tr>
                        <td style="padding-right:10px">
                            <asp:Label runat="server" Font-Bold="true" Font-Size="Larger"  Text="Search"></asp:Label>
                        </td>
                        <td style="padding-right:3px">
                            <asp:Label runat="server" Text="Title"></asp:Label>
                        </td>
                        <td style="padding-right:10px">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTitleSearch"></asp:TextBox>
                        </td>
                        <td style="padding-right:3px">
                             <asp:Label runat="server" Text="Supervisor"></asp:Label>
                        </td>
                        <td style="padding-right:10px">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSupervisorSearch"></asp:TextBox>
                        </td>
                         <td style="padding-right:3px">
                             <asp:Label runat="server" Text="Project Type"></asp:Label>
                        </td>
                        <td style="padding-right:10px">
                            <asp:DropDownList runat="server" ID="txtTypeSearch" CssClass="form-control">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Develop and Test" Value="Develop and Test"></asp:ListItem>
                                <asp:ListItem Text="Experimental" Value="Experimental"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="padding-right:10px">
                            <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                        </td>
                        <td style="padding-right:10px">
                            <asp:Button runat="server" Text="Clear Filter" ID="btnClearFilter" OnClick="btnClearFilter_Click" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>

                <br />
            </div>
        </div>
        

        <div class="row">
            <div class="col-md-4" style="width:100%">
                <asp:GridView ID="gvwExample" runat="server"  AllowPaging="true"  AutoGenerateColumns="false" Width="100%" PageSize="15" 
                    CssClass="table table-bordered table-hover table-responsive table-condensed" OnPageIndexChanging="gridView_PageIndexChanging"
                     OnRowDeleting="gvwExample_RowDeleting" DataKeyNames="_id" OnRowDataBound="gvwExample_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Title" />
                        <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                        <asp:BoundField DataField="SubjectAreas" HeaderText="Subject Areas" />
                        <asp:BoundField DataField="SuitableFor" HeaderText="Suitable For" />
                        <asp:BoundField DataField="Type" HeaderText="Project Type" />
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server"  CommandArgument='<%#Eval("_id") %>'
                                    CommandName="View" OnCommand="lnkView_Command">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Add to My Topics">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAdd" runat="server"  CommandArgument='<%#Eval("_id") %>'
                                    CommandName="Add" OnCommand="lnkAdd_Command">Add</asp:LinkButton>
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
