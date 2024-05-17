import {
    NumberInput,
    NumberInputControl,
    NumberInputDecrementTrigger,
    NumberInputField,
    NumberInputIncrementTrigger
  } from '@ark-ui/react'

  export function NumericInput(props){

    return(
        <NumberInput className='SearchUrl-numberInput' min={props.min} max={props.max} onChange={(e) => props.onChange(parseInt(e.value))} value={props.value}>
            <NumberInputField className='SearchUrl-numberInputInput'/>
            <NumberInputControl>
                <NumberInputDecrementTrigger>–</NumberInputDecrementTrigger>
                <NumberInputIncrementTrigger>+</NumberInputIncrementTrigger>
            </NumberInputControl>
        </NumberInput>
    )
  }