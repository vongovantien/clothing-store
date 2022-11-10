import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import * as React from 'react';
import { ProductConstants } from '../utils/constants/ProductContants';

export default function StickyHeadTable(props) {
    // const [page, setPage] = React.useState(0);
    // const [rowsPerPage, setRowsPerPage] = React.useState(10);

    // const handleChangePage = (event, newPage) => {
    //     setPage(newPage);
    // };

    // const handleChangeRowsPerPage = (event) => {
    //     setRowsPerPage(+event.target.value);
    //     setPage(0);
    // };

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
                        {props.rows
                            .map((row) => {
                                return (
                                    <TableRow hover role="checkbox" tabIndex={-1} key={row.id}>
                                        {/* <TableCell>
                                            abc
                                        </TableCell> */}
                                        {props.columns.map((column) => {
                                            const value = row[column.id];
                                            return (
                                                <TableCell key={column.id}>
                                                    {column.format && typeof value === 'number'
                                                        ? column.format(value)
                                                        : value}
                                                </TableCell>
                                            );
                                        })}
                                    </TableRow>
                                );
                            })}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[10, 25, 100]}
                component="div"
                count={props.rows.length}
                rowsPerPage={props.pageSize}
                page={props.pageNumber}
                onPageChange={(event, value) => props.handlePaging(value, ProductConstants.PAGE_NUMBER)}
                onRowsPerPageChange={(event) => props.handlePaging(event.target.value, ProductConstants.PAGE_SIZE)}
            />
        </Paper >
    );
}