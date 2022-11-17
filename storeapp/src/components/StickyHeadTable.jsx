import DeleteIcon from '@mui/icons-material/Delete';
import InfoIcon from '@mui/icons-material/Info';
import { Button } from '@mui/material';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import { Stack } from '@mui/system';
import { ProductConstants } from '../constants/ProductConstants';

export default function StickyHeadTable(props) {
    return (
        <Paper sx={{ width: '100%', overflow: 'hidden' }}>
            <TableContainer sx={{ maxHeight: 440 }}>
                <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                        <TableRow>
                            {props.columns.map((column) => (
                                <TableCell
                                    key={column.id}
                                    align={column.align}
                                    style={{ minWidth: column.minWidth }}
                                >
                                    {column.label}
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {props.rows && props.rows.slice(props.pageNumber * props.pageSize, props.pageNumber * props.pageSize + props.pageSize)
                            .map((row) => {
                                return (
                                    <TableRow hover key={row.id}>
                                        <TableCell>
                                            <Stack direction="row" alignItems="center" justifyContent="flex-end" spacing={2} mr={2}>
                                                <Button variant="outlined" color="success" onClick={() => props.handleDetail(row.id)}><InfoIcon /></Button>
                                                <Button variant="outlined" color="error" onClick={() => props.handleDelete(row.id)}><DeleteIcon /></Button>
                                            </Stack>
                                        </TableCell>
                                        {
                                            props.columns.filter(s => !!s.id).map((column) => {
                                                const value = row[column.id];
                                                return (
                                                    <TableCell key={column.id}>
                                                        {column.format && typeof value === 'number'
                                                            ? column.format(value)
                                                            : value}
                                                    </TableCell>
                                                );
                                            })
                                        }
                                    </TableRow>
                                );
                            })}
                    </TableBody>
                </Table>
            </TableContainer>
            {props.rows.length} - {props.pageSize} - {props.pageNumber}
            < TablePagination
                rowsPerPageOptions={[5, 10, 25, 100]}
                component="div"
                count={props.rows.length}
                rowsPerPage={props.pageSize}
                page={props.pageNumber}
                onPageChange={(event, value) => props.handlePaging(value, ProductConstants.PAGE_NUMBER)
                }
                onRowsPerPageChange={(event) => props.handlePaging(event.target.value, ProductConstants.PAGE_SIZE)}
            />
        </Paper >
    );
}