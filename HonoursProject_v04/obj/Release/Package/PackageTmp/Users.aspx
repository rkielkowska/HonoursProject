<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HonoursProject_v04.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form runat="server" >
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label style="font-size:larger">Add New User</label>
                </div>

                <div class="form-group">
                    <label for="txtUsername">Username</label>
                    <input runat="server" type="text" class="form-control" id="txtUsername" placeholder="Enter Username"/>
                </div>

                <div class="form-group">
                    <label for="txtFullName">Full Name</label>
                    <input runat="server" type="text" class="form-control" id="txtFullName" placeholder="Enter Full Name"/>
                </div>

                <div class="form-group">
                    <label for="txtPassword">Password</label>
                    <input runat="server" type="password" class="form-control" id="txtPassword" placeholder="Enter Password"/>
                </div>

                <div class="form-group">
                    <label for="txtPasswordRepeat">Repeat Password</label>
                    <input runat="server" type="password" class="form-control" id="txtPasswordRepeat" placeholder="Repeat Password"/>
                </div>

                <div class="form-group">
                    <label for="txtEmail">Email Address</label>
                    <input runat="server" type="email" class="form-control" id="txtEmail" placeholder="Enter Email"/>
                </div>

                <div class="form-group">
                    <label for="txtUserType">User Type</label>
                    <asp:DropDownList runat="server" ID="txtUserType" CssClass="form-control">
                        <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                        <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                        <asp:ListItem Text="Project Co-ordinator" Value="Project Co-ordinator"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="txtStudentID">Student ID (Students only)</label>
                    <input runat="server" type="text" class="form-control" id="txtStudentID" placeholder="Enter Student ID"/>
                </div>

                <div class="form-group">
                    <label for="txtCourse">Course (Students only)</label>
                    <input runat="server" type="text" class="form-control" id="txtCourse" placeholder="Enter Course"/>
                </div>

                <asp:Button runat="server" Text="Submit" ID="btnAddNew" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />

                <label runat="server" id="lblError" style="color:red; font:bold" ></label>
            </div>
        </div> <br />

        <div class="row">
            <div class="col-md-4" style="width:100%">
                <asp:GridView ID="gvwExample" runat="server"  AllowPaging="true"  AutoGenerateColumns="false" Width="100%" PageSize="15" 
                    CssClass="table table-bordered table-hover table-responsive table-condensed" OnPageIndexChanging="gridView_PageIndexChanging"
                     OnRowDeleting="gvwExample_RowDeleting" DataKeyNames="_id" >
                    <Columns>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
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
