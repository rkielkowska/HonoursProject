<%@ Page Title="" Async="true"  Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="HonoursProject_v04.Students" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form runat="server" style="width:100%">
        <asp:ScriptManager runat="server" EnablePageMethods="true"  ></asp:ScriptManager>

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" Font-Bold="true" Font-Size="22px" Text="Students"></asp:Label><br /><br />
            </div>
        </div>

        <div class="row" >
            <div class="col-md-4">
                <table style="width:100%">
                    <tr>
                        <td style="padding-right: 15px">
                            <asp:Button runat="server" Text="Students With No Project Allocated" OnClick="btnFilterNoProject_Click" ID="btnFilterNoProject" CssClass="btn btn-primary" />
                        </td>
                        <td style="padding-right: 40px">
                            <asp:Button runat="server" Text="Show All" OnClick="btnShowAll_Click" ID="btnShowAll" CssClass="btn btn-primary" />
                        </td>
                        <td style="padding-right: 5px">
                            <asp:Label runat="server" Font-Bold="true"  Text="Allocate Project: "></asp:Label>
                        </td>
                        <td style="padding-right: 5px">
                            <asp:DropDownList Width="350px" runat="server" ID="txtTopics" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                        <td style="padding-right: 20px">
                            <asp:Button runat="server" Text="Allocate" OnClick="btnAllocate_Click" ID="btnAllocate" CssClass="btn btn-primary" />
                        </td>
                        <td>
                            <asp:Label runat="server" Font-Bold="true" ForeColor="Red" Width="100px" Text="" ID="lblError"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br /><br />
            </div>
        </div>

        <div class="row">
            <div class="col-md-4" style="width:100%">
                <asp:GridView ID="gvwExample" runat="server"  AllowPaging="true"  AutoGenerateColumns="false" Width="100%" PageSize="15" 
                    CssClass="table table-bordered table-hover table-responsive table-condensed" OnPageIndexChanging="gridView_PageIndexChanging"
                     DataKeyNames="_id" >
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox AutoPostBack="true" ID="chkSelect" runat="server" CausesValidation="False"
                                    OnCheckedChanged="chkSelect_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                        <asp:BoundField DataField="Course" HeaderText="Course" />
                        <asp:BoundField DataField="AllocatedTopic" HeaderText="Project Topic" />
                        <asp:BoundField DataField="IsSelfProposed" HeaderText="Topic Self Proposed" />
                        <asp:TemplateField HeaderText="Project Proposal">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Eval("ProjectProposal") %>' ID="chkProjProposal" OnCheckedChanged="chkProjProposal_CheckedChanged" 
                                    runat="server" CausesValidation="False" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Ethics Form">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Eval("EthicsForm") %>' ID="chkEthicsForm" OnCheckedChanged="chkEthicsForm_CheckedChanged" 
                                    runat="server" CausesValidation="False" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interim Report">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Eval("InterimReport") %>' ID="chkInterimReport" OnCheckedChanged="chkInterimReport_CheckedChanged" 
                                    runat="server" CausesValidation="False" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Final Report">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Eval("FinalReport") %>' ID="chkFinalReport" OnCheckedChanged="chkFinalReport_CheckedChanged" 
                                    runat="server" CausesValidation="False" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Poster Presentation">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#Eval("PosterPresentation") %>' ID="chkPosterPresentation" OnCheckedChanged="chkPosterPresentation_CheckedChanged" 
                                    runat="server" CausesValidation="False" AutoPostBack="true" />
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
