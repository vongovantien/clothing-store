import React from 'react'

export const Alert = (title, message) => {
  return (
    <Alert severity="{{title}}">{{ message }}</Alert>
  )
}
