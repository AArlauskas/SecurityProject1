import { DataGrid } from "@mui/x-data-grid";
export default function AdminPage() {
  const columns = [
    {
      field: "id",
      headerName: "Id",
    },
    {
      field: "role",
      headerName: "Role",
    },
    {
      field: "username",
      headerName: "username",
    },
    {
      field: "password",
      headerName: "Password",
    },
  ];
  const data = [
    {
      id: 1,
      role: "user",
      username: "Aurimas",
      password: "password",
    },
  ];
  return <DataGrid columns={columns} rows={data} />;
}
