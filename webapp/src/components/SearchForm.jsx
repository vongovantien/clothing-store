import { MenuItem, TextField } from '@mui/material';
import { Box } from '@mui/system';
import { default as React, useState } from 'react';

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
    value: "Name ASC"
},
{
    title: "Giá, cao đến thấp",
    value: "Name DESC"
}]
const SearchForm = () => {
    const [pageSize, setPageSize] = useState(5)
    const [sortBy, setSortBy] = useState("Name, A đến Z")

    const handleChange = (event, type) => {
        switch (type) {
            case "sort":
                setSortBy(event.target.value);
                break;
            case "pageSize":
                setPageSize(event.target.value);
                break;
            default:
                break;
        }
    };
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
                    value={pageSize}
                    onChange={e => handleChange(e, "pageSize")}
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
                    value={sortBy}
                    onChange={e => handleChange(e, "sort")}
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