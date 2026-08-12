import { TailSpin } from "react-loader-spinner"

export const Loading = () => {
    return (
        <div className="flex items-center justify-center h-screen bg-[#021526]">
            <TailSpin
                height="80"
                width="80"
                color="#6EACDA"
                ariaLabel="tail-spin-loading"
                visible={true}
            />
        </div>
    )
}