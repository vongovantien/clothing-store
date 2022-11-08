import { MenuItem, TextField } from '@mui/material';
import { Box } from '@mui/system';
import { default as React } from 'react';
import { PAGE_SIZE, SORT_BY } from '../utils/constants/ProductContants';

const pageSizes = [5, 10, 15, 25, 40]
const sortBies = [{
    title: "Name, A đến Z",
    value: "Name ASC"
},
{
    title: "Name, Z đến A",
    value: "Name DESC"
},
{
    title: "Giá, thấp đến cao",
    value: "Price ASC"
},
{
    title: "Giá, cao đến thấp",
    value: "Price DESC"
}]
const SearchForm = (props) => {
    return (
        <>
            <Box
                component="form"
                sx={{
                    '& .MuiTextField-root': { m: 1, width: '25ch' },
                }}
                noValidate
                autoComplete="off"
            >
                <TextField
                    id="outlined-select-currency"
                    select
                    label="Item per Page"
                    value={props.pageSize}
                    onChange={e => props.handleSearch(e.target.value, PAGE_SIZE)}
                >
                    {pageSizes.map((option, index) => (
                        <MenuItem key={index} value={option}>
                            {option}
                        </MenuItem>
                    ))}
                </TextField>
                <TextField
                    id="outlined-select-currency"
                    select
                    label="Sort By"
                    value={props.sortBy}
                    onChange={e => props.handleSearch(e.target.value, SORT_BY)}
                >
                    {sortBies.map((option, index) => (
                        <MenuItem key={index} value={option.value}>
                            {option.title}
                        </MenuItem>
                    ))}
                </TextField>
            </Box>
        </>
    )
}

export default SearchForm