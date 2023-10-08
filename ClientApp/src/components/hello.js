import { useEffect, useState } from "react";

const Hello = () => {
  const [data, setData] = useState('')
  useEffect(() => {
    fetch('job')
      .then((results) => {
        return results.json()
      })
      .then(data => {
        setData(data)
      })
  }, [])

  return (
    {data}
  )
}
export default Hello;