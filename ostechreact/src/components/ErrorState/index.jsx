export const ErrorState = ({ message }) => {
    return (
        <div className="flex items-center justify-center py-16">
            <h2 className="text-red-500 text-lg font-semibold">{message}</h2>
        </div>
    );
}