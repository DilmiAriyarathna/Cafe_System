Imports System.Data.SqlClient
Imports System.Data
Imports System.IO


Public Class Form24
    Dim code, discrip, po As String
    Dim ucost, total As Double
    Dim qty As Integer
    Dim table As New DataTable("Table")

    Dim d As DateTime = DateTime.Now
    Dim formata As String = "MM/dd/yyyy"
    Dim x, tmpcnt, cnt, r, j As Integer
    Dim t As DateTime = DateTime.Now
    Dim formatb As String = "hh:mm:ss tt"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub Form24_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT PO_num FROM Purchase_Order where I_flag = '1' GROUP BY PO_num"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT UName FROM Usert "
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''
        ComboBox1.Select()
        ComboBox1.DroppedDown = True
        ''
        BunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid1.DefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid2.ColumnHeadersDefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        BunifuCustomDataGrid2.DefaultCellStyle.Font = New Font("Verdena", 7)
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "select Max(chk)chk from TGC"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label6.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
        ''
        Dim i = Convert.ToInt32(Label6.Text)
        i += 1
        TextBox2.Text = i.ToString()
    End Sub
    Private Sub crgrid()
        table.Columns.Add("Code", Type.GetType("System.String"))
        table.Columns.Add("Discription", Type.GetType("System.String"))
        table.Columns.Add("PO_QTY", Type.GetType("System.Int32"))
        table.Columns.Add("PO_Unit_Cost", Type.GetType("System.String"))
        table.Columns.Add("Check_QTY", Type.GetType("System.Int32"))
        table.Columns.Add("Check_Unit_Cost", Type.GetType("System.String"))
        table.Columns.Add("FOC_Qty", Type.GetType("System.Int32"))

        BunifuCustomDataGrid2.DataSource = table
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Dim dt As New DataTable()
        'dt.Columns.Add("Code")
        'dt.Columns.Add("Discription")
        'dt.Columns.Add("PO_QTY")
        'dt.Columns.Add("PO_Unit_Cost")
        'dt.Columns.Add("Check_QTY")
        'dt.Columns.Add("Check_Unit_Cost")
        'dt.Columns.Add("FOC_Qty")
        'For Each row As DataGridViewRow In BunifuCustomDataGrid2.Rows
        '    Dim isSelected As Boolean = Convert.ToBoolean(row.Cells("checkBoxColumn").Value)
        '    If isSelected Then
        '        dt.Rows.Add(row.Cells(1).Value, row.Cells(2).Value, row.Cells(3).Value)
        '    End If
        'Next
        'BunifuCustomDataGrid2.DataSource = dt
        Dim dre As New System.Windows.Forms.DataGridViewRow
        For Each dre In Me.BunifuCustomDataGrid1.Rows
            table.Rows.Add(dre.Cells(3).Value, dre.Cells(4).Value, dre.Cells(5).Value, dre.Cells(6).Value)
            BunifuCustomDataGrid2.DataSource = table
        Next
    End Sub
    Private Sub clr()
        Dim form24 As New Form24
        Me.Hide()
        form24.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        clr()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Label9.Text = "" Then
            MsgBox("Invalid Checking !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        totalcst()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update TGC set [chk] = [chk] + 1"
        cmd6 = New SqlCommand(SQL, conn)
        cmd6.ExecuteNonQuery()
        conn.Close()
        ''
        For r = 0 To BunifuCustomDataGrid1.RowCount - 1
            conn.Open()
            SQL = "insert into Purchase_chk(Date,Time,chk_by,chk_Acc,PO_Num,CHK,PO_Date,Supp,Code,PO_qty,PO_ucst, " &
                    "chk_qty,chk_ucst,foc,flag)values( " &
       "@Date,@Time,@chk_by,@chk_Acc,@PO_Num,@CHK,@PO_Date,@Supp,@Code,@PO_qty,@PO_ucst,@chk_qty,@chk_ucst,@foc,@flag)"
            cmd = New SqlCommand(SQL, conn)
            With cmd.Parameters
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                .AddWithValue("@chk_by", ComboBox2.Text)
                .AddWithValue("@chk_Acc", Label9.Text)
                .AddWithValue("@PO_Num", TextBox1.Text)
                .AddWithValue("@CHK", TextBox2.Text)
                .AddWithValue("@PO_Date", BunifuCustomDataGrid1.Rows(r).Cells(0).Value)
                .AddWithValue("@Supp", Label10.Text)
                .AddWithValue("@Code", BunifuCustomDataGrid2.Rows(r).Cells(0).Value)
                .AddWithValue("@PO_qty", BunifuCustomDataGrid2.Rows(r).Cells(2).Value)
                .AddWithValue("@PO_ucst", BunifuCustomDataGrid2.Rows(r).Cells(3).Value)
                .AddWithValue("@chk_qty", BunifuCustomDataGrid2.Rows(r).Cells(4).Value)
                .AddWithValue("@chk_ucst", BunifuCustomDataGrid2.Rows(r).Cells(5).Value)
                .AddWithValue("@foc", BunifuCustomDataGrid2.Rows(r).Cells(6).Value)
                .AddWithValue("@flag", "C")
            End With
            cmd.ExecuteNonQuery()
            conn.Close()
        Next
        ''
        prfm()
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "update Purchase_Order set I_flag ='5'  where PO_num = '" + TextBox1.Text + "'"
        cmd3 = New SqlCommand(SQL, conn)
        cmd3.ExecuteNonQuery()
        conn.Close()
        ''
        MsgBox("Update Successfully !")
        clr()
    End Sub
    Private Sub totalcst()
        total = BunifuCustomDataGrid2.Rows(j).Cells(2).Value * BunifuCustomDataGrid2.Rows(j).Cells(3).Value
    End Sub
    Private Sub prfm()
        For j = 0 To BunifuCustomDataGrid2.RowCount - 1
            conn.Open()
            SQL = "insert into Stock_Perform(Date,time,Code,Discription,Qty,Unit_Cost,Total,FOC,Supplier, " &
            "Store,Login,PO_num,Flag,Type,TXT,PO_Date, " &
            "PO_qty,po_chk,po_chk_qty,acc_no) values ( " &
            "@Date,@time,@Code,@Discription,@Qty,@Unit_Cost,@Total,@FOC,@Supplier,@Store, " &
            "@Login,@PO_num,@Flag,@Type, " &
            "@TXT,@PO_Date,@PO_qty,@po_chk,@po_chk_qty,@acc_no)"
            cmd4 = New SqlCommand(SQL, conn)
            With cmd4.Parameters
                .AddWithValue("@Date", d.ToString(formata))
                .AddWithValue("@Time", t.ToString(formatb))
                ' BunifuCustomDataGrid2.Rows(r).Cells(0).Value
                .AddWithValue("@Code", BunifuCustomDataGrid2.Rows(j).Cells(0).Value)
                .AddWithValue("@Discription", BunifuCustomDataGrid2.Rows(j).Cells(1).Value)
                .AddWithValue("@Qty", BunifuCustomDataGrid2.Rows(j).Cells(2).Value)
                .AddWithValue("@Unit_Cost", BunifuCustomDataGrid2.Rows(j).Cells(3).Value)
                .AddWithValue("@Total", total)
                .AddWithValue("@FOC", BunifuCustomDataGrid2.Rows(j).Cells(6).Value)
                .AddWithValue("@Supplier", Label10.Text)
                .AddWithValue("@Store", Label11.Text)
                '' .AddWithValue("@GRN_No", BunifuCustomDataGrid2.Rows(j).Cells(13).Value)

                '' .AddWithValue("@GRN_Id", Label13.Text)
                .AddWithValue("@Login", ComboBox2.Text)
                '' .AddWithValue("@Cashier", User_Name)

                ''    .AddWithValue("@PCK", BunifuCustomDataGrid2.Rows(j).Cells(18).Value)
                ''   .AddWithValue("@No_pks", BunifuCustomDataGrid2.Rows(j).Cells(19).Value)
                ''    .AddWithValue("@GRN_Invoice", BunifuCustomDataGrid2.Rows(j).Cells(14).Value)

                .AddWithValue("@PO_num", TextBox1.Text)
                .AddWithValue("@Flag", "1")
                .AddWithValue("@Type", "PO_Checking")
                .AddWithValue("@TXT", "PO_CHK:" + TextBox2.Text + "/" + Label11.Text)

                .AddWithValue("@PO_Date", BunifuCustomDataGrid1.Rows(j).Cells(0).Value)
                ''  .AddWithValue("@GRN_Date", DateTimePicker1.Value)

                ''    .AddWithValue("@Total_Amt", TextBox23.Text)
                ''    .AddWithValue("@Discount", TextBox24.Text)
                ''    .AddWithValue("@Dis_Amt", TextBox25.Text)
                ''    .AddWithValue("@Net_Amt", TextBox26.Text)
                ''    .AddWithValue("@VAT", TextBox27.Text)
                ''    .AddWithValue("@Grand", TextBox28.Text)

                .AddWithValue("@PO_qty", BunifuCustomDataGrid2.Rows(j).Cells(2).Value)
                .AddWithValue("@po_chk", TextBox2.Text)
                .AddWithValue("@po_chk_qty", BunifuCustomDataGrid2.Rows(j).Cells(4).Value)
                .AddWithValue("@acc_no", Label9.Text)
            End With
            cmd4.ExecuteNonQuery()
            conn.Close()
        Next
    End Sub
    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Acc_No from Usert where UName ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label9.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ''
            conn = GetConnect()
            conn.Open()
            da = New SqlDataAdapter("select PO_Date,Supplier_Name,PO_Num,Code,Discription,QTY,CAST(Unit_Cost as numeric(17,2))Unit_Cost " &
                                    "from PO_chk where PO_Num = '" + ComboBox1.Text + "'", conn)
            dt = New DataTable()
            da.Fill(dt)
            BunifuCustomDataGrid1.DataSource = dt
            conn.Close()
            ''
            TextBox1.Text = ComboBox1.Text
            crgrid()
            grd()
            ''
            conn = GetConnect()
            conn.Open()
            SQL = "select Code,Discription,QTY,Unit_Cost,supp,Store from PO_chk where PO_Num = '" + ComboBox1.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            Using dr = cmd.ExecuteReader
                While (dr.Read())
                    code = dr.GetValue(0).ToString  'name
                    discrip = dr.GetValue(1).ToString  'code
                    qty = dr.GetValue(2).ToString
                    ucost = dr.GetValue(3).ToString
                    Label10.Text = dr.GetValue(4).ToString
                    Label11.Text = dr.GetValue(5).ToString
                End While
            End Using
            conn.Close()

            Button2_Click(sender, e)

            ComboBox1.Enabled = False
        End If
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(1).Width = 200
        BunifuCustomDataGrid1.Columns(4).Width = 200

        BunifuCustomDataGrid2.Columns(0).Width = 85
        BunifuCustomDataGrid2.Columns(1).Width = 200
        BunifuCustomDataGrid2.Columns(2).Width = 85
        BunifuCustomDataGrid2.Columns(3).Width = 85
        BunifuCustomDataGrid2.Columns(4).Width = 85
        BunifuCustomDataGrid2.Columns(5).Width = 100
    End Sub
End Class